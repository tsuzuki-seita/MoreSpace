using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject FPSCamera;
    [SerializeField] Canvas playerCamvas;
    GameObject playerNameText;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine) //このオブジェクトが自分がPhotonを介して生成したものならば
        {
            transform.GetComponent<PlayerMove>().enabled = true; //MovementController.csを有効にする
            FPSCamera.GetComponent<Camera>().enabled = true; //FPSCameraのCameraコンポーネントを有効にする
            //rigidbody.useGravity = true;
            GetComponent<CountCrystal>().enabled = true;
            GetComponent<PlayerHPManager>().enabled = true;
            //GetComponent<AudioSource>().enabled = true;
            GetComponent<PlayerAttack>().enabled = true;
            //foreach (SimpleMouseRotator rot in GetComponentsInChildren<SimpleMouseRotator>())
            //    rot.enabled = true;

            playerNameText = GameObject.Find("PlayerNameText");
            playerCamvas.enabled = false;
        }
        else
        {
            transform.GetComponent<PlayerMove>().enabled = false;
            FPSCamera.GetComponent<Camera>().enabled = false;
        }

        if (playerNameText != null) //Textオブジェクトが空でなければ
        {
            if (photonView.IsMine) //このオブジェクトが自分がPhotonを介して生成したものならば
            {
                playerNameText.GetComponent<Text>().text = PhotonNetwork.NickName; //ログイン
                //playerNameText.text = photonView.Owner.NickName;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
