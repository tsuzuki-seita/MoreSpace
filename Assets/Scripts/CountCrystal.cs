using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class CountCrystal : MonoBehaviourPunCallbacks
{
    public int crystalCount;
    float buttonTime;
    [SerializeField] float needTime;
    Image image2;
    Text playerName;
    PlayerHPManager playerHPManager;
    PlayerMove playerMove;
    ChangeWepon changeWepon;

    [SerializeField] Button uniqueButton;
    [SerializeField] Button ultButton;
    [SerializeField] Image radarImage;
    [SerializeField] Image silentImage;
    [SerializeField] Image lazerImage;
    [SerializeField] Image stealthImage;
    [SerializeField] Image missileImage;
    [SerializeField] GameObject arrow;

    private PhotonView m_photonView = null;

    public enum getState
    {
        Nomal,    
        Getting,
        Gotcha,
    }
    getState state;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine) //このオブジェクトが自分がPhotonを介して生成したものならば
        {
            image2 = GameObject.Find("ValueImage").GetComponent<Image>();
            playerName = GameObject.Find("PlayerNameText").GetComponent<Text>();

            playerHPManager = GetComponent<PlayerHPManager>();
            playerMove = GetComponent<PlayerMove>();
            changeWepon = GetComponent<ChangeWepon>();
        }

        crystalCount = 0;
        image2.fillAmount = 0;
        // 型の変数作成
        state = getState.Nomal;

        m_photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == getState.Getting)
        {
            buttonTime += Time.deltaTime;
            image2.fillAmount = buttonTime / needTime;
            if (buttonTime > needTime)
            {
                crystalCount++;
                state = getState.Gotcha;
                buttonTime = 0;
                image2.fillAmount = 0;
                if (crystalCount == 1)
                {
                    Count1();
                }
                if (crystalCount == 2)
                {
                    Count2();
                }
                if (crystalCount == 3)
                {
                    Count3();
                }
                if (crystalCount == 4)
                {
                    m_photonView.RPC("LodingScene", RpcTarget.All, playerName.text);
                }
            }
        }
        if(state == getState.Nomal)
        {
            buttonTime = 0;
            image2.fillAmount = 0;
        }
        
    }

    [PunRPC]
    private void LodingScene(string name)
    {
        ResultManager.winnerName = name;
        SceneManager.LoadScene("ResultScene");
    }

    private void Count1()
    {
        var random1 = Random.Range(0, 3);
        if(random1 == 0)
        {
            playerHPManager.hp += 50;   //HP増加
        }
        if (random1 == 1)
        {
            playerMove.bonus = 3;   //スピード増加
        }
        if(random1 == 2)
        {
            needTime = 2;   //採取速度アップ
        }
    }

    private void Count2()
    {
        uniqueButton.image.enabled = true;

        var random2 = Random.Range(0, 3);
        if (random2 == 0)
        {
            radarImage.enabled = true;   //レーダー追加
            arrow.SetActive(true);
        }
        if (random2 == 1)
        {
            silentImage.enabled = true; //静音弾追加
            changeWepon.uniqueState = ChangeWepon.UniqueState.Silent;
        }
        if (random2 == 2)
        {
            lazerImage.enabled = true;  //レーザー追加
            changeWepon.uniqueState = ChangeWepon.UniqueState.Lazer;
        }
    }

    private void Count3()
    {
        ultButton.image.enabled = true;

        var random3 = Random.Range(0, 2);
        if (random3 == 0)
        {
            stealthImage.enabled = true;    //ステルス追加
            changeWepon.ultState = ChangeWepon.UltState.Stealth;
        }
        if (random3 == 1)
        {
            missileImage.enabled = true;    //ミサイル追加
            changeWepon.ultState = ChangeWepon.UltState.Missile;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (state == getState.Gotcha)
        {
            Debug.Log("destroy");
            //Destroy(other);
            Destroy(other.gameObject.transform.parent.gameObject);
            Invoke("ResetValue", 0.1f);
        }
        if (other.gameObject.tag == "Crystal")
        {
            Debug.Log("crystal検知");
            if (Input.GetKey(KeyCode.LeftShift))
            {
                state = getState.Getting;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                state = getState.Nomal;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        buttonTime = 0;
        image2.fillAmount = 0;//
        state = getState.Nomal;
    }
    private void ResetValue()
    {
        state = getState.Nomal;
        buttonTime = 0;
        image2.fillAmount = 0;
    }
}
