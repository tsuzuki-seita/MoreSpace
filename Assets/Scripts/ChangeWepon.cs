using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ChangeWepon : MonoBehaviourPunCallbacks
{
    Image nomalValue;
    Image uniqueValue;
    Image ultValue;

    PlayerAttack playerAttack;

    public enum UniqueState
    {
        Silent,
        Lazer,
    }
    public UniqueState uniqueState;

    public enum UltState
    {
        Missile,
        Stealth,
    }
    public UltState ultState;

    // Start is called before the first frame update
    void Start()
    {
        //if (GameObject.FindWithTag("Player").GetComponent<PhotonView>().IsMine) //このオブジェクトが自分がPhotonを介して生成したものならば
        //{
        //    nomalValue = GameObject.Find("NomalWeaponValueImage").GetComponent<Image>();
        //    uniqueValue = GameObject.Find("UniqueWeaponValueImage").GetComponent<Image>();
        //    ultValue = GameObject.Find("UltWeaponValueImage").GetComponent<Image>();
        //}
        nomalValue = GameObject.Find("NomalWeaponValueImage").GetComponent<Image>();
        uniqueValue = GameObject.Find("UniqueWeaponValueImage").GetComponent<Image>();
        ultValue = GameObject.Find("UltWeaponValueImage").GetComponent<Image>();

        nomalValue.enabled = true;
        uniqueValue.enabled = false;
        ultValue.enabled = false;

        //if (GameObject.FindWithTag("Player").GetComponent<PhotonView>().IsMine)
        //{
        //    playerAttack = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        //}
        playerAttack = GetComponent<PlayerAttack>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNomalClick()
    {
        nomalValue.enabled = true;
        uniqueValue.enabled = false;
        ultValue.enabled = false;

        playerAttack.weaponState = PlayerAttack.WeaponState.Nomal;
    }

    public void OnUniqueClick()
    {
        nomalValue.enabled = false;
        uniqueValue.enabled = true;
        ultValue.enabled = false;

        if(uniqueState == UniqueState.Lazer)
        {
            playerAttack.weaponState = PlayerAttack.WeaponState.Lazer;
        }
        if (uniqueState == UniqueState.Silent)
        {
            playerAttack.weaponState = PlayerAttack.WeaponState.Silent;
        }
    }

    public void OnUltClick()
    {
        nomalValue.enabled = false;
        uniqueValue.enabled = false;
        ultValue.enabled = true;

        if(ultState == UltState.Missile)
        {
            playerAttack.weaponState = PlayerAttack.WeaponState.Missile;
        }
        if (ultState == UltState.Stealth)
        {
            playerAttack.weaponState = PlayerAttack.WeaponState.Stealth;
        }
    }
}
