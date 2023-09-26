using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LauncherManager : MonoBehaviourPunCallbacks
{
    public GameObject LoginPanel;
    public InputField playerNameInput;

    public GameObject ConnectingPanel;
    public GameObject LobbyPanel;

    public Text statusText;

    #region Unity Methods

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        LoginPanel.SetActive(true);
        ConnectingPanel.SetActive(false);
        LobbyPanel.SetActive(false);
    }
    #endregion

    #region Public Methods

    public void ConnectToPhotonServer() //LoginButtonで呼ぶ
    {
        if (!PhotonNetwork.IsConnected) //サーバーに接続していたら
        {
            string playerName = playerNameInput.text;
            if (!string.IsNullOrEmpty(playerName))
            {
                PhotonNetwork.LocalPlayer.NickName = playerName;
                PhotonNetwork.ConnectUsingSettings();
                ConnectingPanel.SetActive(true);
                LoginPanel.SetActive(false);
            }
        }
        else { }
    }

    public void JoinRandomRoom() //StatButtonで呼ぶ
    {
        PhotonNetwork.JoinRandomRoom();
    }
    #endregion


    #region Photon Callbacks

    public override void OnConnectedToMaster() //ログインしたら呼ばれる
    {
        Debug.Log(PhotonNetwork.NickName+ "Connected to Photon server");
        LobbyPanel.SetActive(true);
        ConnectingPanel.SetActive(false);
 
    }

    // Photonのサーバーから切断された時に呼ばれるコールバック
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"サーバーとの接続が切断されました: {cause.ToString()}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"ルームの作成に失敗しました: {message}");
        CreateAndJoinRoom(); //ルームがなければ自ら作って入る
    }

    public override void OnJoinedRoom() //ルームに入ったら呼ばれる
    { 
        Debug.Log(PhotonNetwork.NickName+ "joined to"+ PhotonNetwork.CurrentRoom.Name);
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerCount != 2)
        {
            statusText.text = "惑星探索中";
        }
        else
        {
            statusText.text = "惑星が見つかりました。ワープします。";
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                PhotonNetwork.IsMessageQueueRunning = false;
                PhotonNetwork.CurrentRoom.IsOpen = false;

                statusText.text = "惑星が見つかりました。ワープします。";
                PhotonNetwork.LoadLevel("GameScene");
            }
        }
    }

    #endregion

    #region Private Methods
    void CreateAndJoinRoom()
    {
        // ルームのカスタムプロパティの初期値
        var initialProps = new ExitGames.Client.Photon.Hashtable();
        initialProps["DisplayName"] = $"{PhotonNetwork.NickName}の部屋";
        initialProps["Message"] = "誰でも参加OK！";

        // ロビーのルーム情報から取得できるカスタムプロパティ（キーの配列）
        var propsForLobby = new[] { "DisplayName", "Message" };

        // 作成するルームのルーム設定を行う
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.CustomRoomProperties = initialProps;
        roomOptions.CustomRoomPropertiesForLobby = propsForLobby;

        PhotonNetwork.CreateRoom("Room", roomOptions);
    }
    #endregion
}
