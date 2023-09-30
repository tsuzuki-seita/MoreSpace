using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerHPManager : MonoBehaviourPunCallbacks
{
    public Slider slider;
    public Slider fieldSlider;
    public int hp;
    public int nomalDamage;
    public int missileDamage;
    public int lazerDamage;

    private PhotonView m_photonView = null;

    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
        //if (photonView.IsMine) //このオブジェクトが自分がPhotonを介して生成したものならば
        //{
        //    fieldSlider = GameObject.Find("PlayerHP").GetComponent<Slider>();
        //}
        
        slider.maxValue = hp;
        slider.value = hp;
        fieldSlider.maxValue = hp;
        fieldSlider.value = hp;

        m_photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            var others = PhotonNetwork.PlayerListOthers;
            m_photonView.RPC("LodingScene", RpcTarget.All, others[0].NickName);
        }
    }

    [PunRPC]
    private void LodingScene(string name)
    {
        ResultManager.winnerName = name;
        SceneManager.LoadScene("ResultScene");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "NomalBullet")
        {
            hp -= nomalDamage;
            slider.value = hp;
            fieldSlider.value = hp;//
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Missile")
        {
            hp -= nomalDamage;
            slider.value = hp;
            fieldSlider.value = hp;
        }
    }

    private void OnParticleTrigger()
    {
        hp -= lazerDamage;
        slider.value = hp;
        fieldSlider.value = hp;
    }

    //void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        // 自身のアバターのスタミナを送信する
    //        stream.SendNext(hp);
    //    }
    //    else
    //    {
    //        // 他プレイヤーのアバターのスタミナを受信する
    //        hp = (int)stream.ReceiveNext();
    //    }
    //}
}
