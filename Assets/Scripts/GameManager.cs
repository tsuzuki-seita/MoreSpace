using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject bluePlayerPrefab;
    [SerializeField] GameObject redPlayerPrefab;
    [SerializeField] GameObject[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        var players = PhotonNetwork.PlayerList;

        if (PhotonNetwork.IsConnected) //サーバーに接続していたら
        {
            //foreach (var player in PhotonNetwork.PlayerList)
            //{
            //    if (player.ActorNumber == 1)
            //    {
            //        int randomPoint = Random.Range(-20, 20);
            //        PhotonNetwork.Instantiate(bluePlayerPrefab.name, new Vector3(randomPoint, 0, randomPoint), Quaternion.identity); //Photonを介した生成
            //        //PhotonNetwork.Instantiate(bluePlayerPrefab.name, new Vector3(-138.3f, 388, -110.4457f), Quaternion.identity); //Photonを介した生成
            //    }
            //    else if (player.ActorNumber == 2)
            //    {
            //        int randomPoint = Random.Range(-20, 20);
            //        PhotonNetwork.Instantiate(redPlayerPrefab.name, new Vector3(randomPoint, 0, randomPoint), Quaternion.identity); //Photonを介した生成
            //        //PhotonNetwork.Instantiate(redPlayerPrefab.name, new Vector3(270, -410, -106f), Quaternion.Euler(0, 0, 220));
            //    }
            //}
            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                int index = 0;
                PhotonNetwork.Instantiate(bluePlayerPrefab.name, spawnPoints[index].transform.position, spawnPoints[index].transform.rotation, 0); //Photonを介した生成
            }
            else
            {
                int index = 1;
                PhotonNetwork.Instantiate(redPlayerPrefab.name, spawnPoints[index].transform.position, spawnPoints[index].transform.rotation, 0);
            }
            //if (PhotonNetwork.PlayerList.Length == 1)
            //{
            //    int index = 0;
            //    PhotonNetwork.Instantiate(bluePlayerPrefab.name, spawnPoints[index].transform.position, spawnPoints[index].transform.rotation, 0);
            //}
            //else
            //{
            //    int index = 1;
            //    PhotonNetwork.Instantiate(redPlayerPrefab.name, spawnPoints[index].transform.position, spawnPoints[index].transform.rotation, 0);
            //}
            //if (bluePlayerPrefab != null) //生成するモノが紐づけられているか確認
            //{
            //    PhotonNetwork.IsMessageQueueRunning = true;

            //    int randomPoint = Random.Range(-20, 20); //2つの数値間でランダムな値を代入
            //    PhotonNetwork.Instantiate(bluePlayerPrefab.name, new Vector3(randomPoint, 0, randomPoint), Quaternion.identity); //y座標のみ0の下でプレハブを生成
            //}
        }
    }
}
