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
                Debug.Log(crystalCount);
                state = getState.Gotcha;
                buttonTime = 0;
                image2.fillAmount = 0;
            }
        }
        if(state == getState.Nomal)
        {
            buttonTime = 0;
            image2.fillAmount = 0;
        }
        if(crystalCount == 4)
        {
            m_photonView.RPC("LodingScene", RpcTarget.All, playerName.text);
        }
    }

    [PunRPC]
    private void LodingScene(string name)
    {
        ResultManager.winnerName = name;
        SceneManager.LoadScene("ResultScene");
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
        image2.fillAmount = 0;
        state = getState.Nomal;
    }
    private void ResetValue()
    {
        state = getState.Nomal;
        buttonTime = 0;
        image2.fillAmount = 0;
    }
}
