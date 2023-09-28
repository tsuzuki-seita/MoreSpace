using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerAttack : MonoBehaviourPunCallbacks
{
    public GameObject muzzle;
    public GameObject nomalBullet;

    [SerializeField] Button nomalButton;

    public enum WeaponState
    {
        Nomal,
        Silent,
        Lazer,
        Missile,
        Stealth,
    }
    public WeaponState weaponState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PhotonNetwork.Instantiate(nomalBullet.name, muzzle.transform.position, Quaternion.identity);
            //Instantiate(nomalBullet, muzzle.transform.position, muzzle.transform.rotation);
        }
    }
}
