using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerAttack : MonoBehaviourPunCallbacks
{
    public GameObject muzzle;
    public GameObject nomalBullet;
    public GameObject missile;
    public GameObject Lazer;
    GameObject arrow;
    PlayerHPManager playerHPManager;

    [SerializeField] Button nomalButton;
    [SerializeField] MeshRenderer mesh;

    Transform target;

    public enum WeaponState
    {
        Nomal,
        Silent,
        Lazer,
        Missile,
        Stealth,
    }
    public WeaponState weaponState;

    //public void OnPhotonInstantiate(PhotonMessageInfo info)
    //{
    //    mesh = GetComponent<MeshRenderer>();
    //}

    // Start is called before the first frame update
    void Start()
    {
        weaponState = WeaponState.Nomal;
        playerHPManager = GetComponent<PlayerHPManager>();
        if (!photonView.IsMine)
        {
            target = GameObject.FindWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //PhotonNetwork.Instantiate(nomalBullet.name, muzzle.transform.position, Quaternion.identity);
            //Instantiate(nomalBullet, muzzle.transform.position, muzzle.transform.rotation);
            if (weaponState == WeaponState.Nomal)
            {
                PhotonNetwork.Instantiate(nomalBullet.name, muzzle.transform.position, Quaternion.identity);
                EnemyAlart();
            }
            if (weaponState == WeaponState.Silent)
            {
                PhotonNetwork.Instantiate(nomalBullet.name, muzzle.transform.position, Quaternion.identity);
            }
            if (weaponState == WeaponState.Lazer)
            {
                Lazer.SetActive(true);
            }
            if (weaponState == WeaponState.Missile)
            {
                MissileMove homing;
                for(int i = 0; i < 10; i++)
                {
                    PhotonNetwork.Instantiate(missile.name, muzzle.transform.position, muzzle.transform.rotation);
                    homing = PhotonNetwork.Instantiate(missile.name, muzzle.transform.position, muzzle.transform.rotation).GetComponent<MissileMove>();
                    homing.Target = target;
                }
                
            }
            if (weaponState == WeaponState.Stealth)
            {
                StartCoroutine("Invisible");
            }

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Lazer.SetActive(false);
        }
    }

    IEnumerator Invisible()
    {
        photonView.RPC("RPCSetInvisible", RpcTarget.All, true);
        yield return new WaitForSeconds(5);
        photonView.RPC("RPCSetInvisible", RpcTarget.All, false);
    }

    [PunRPC]
    void RPCSetInvisible(bool invisible)
    {
        if (invisible)
        {
            playerHPManager.nomalDamage = 0;
            playerHPManager.missileDamage = 0;
            playerHPManager.lazerDamage = 0;
            mesh.enabled = false;
        }
        else
        {
            playerHPManager.nomalDamage = 15;
            playerHPManager.missileDamage = 20;
            playerHPManager.lazerDamage = 10;
            mesh.enabled = true;
        }
        
    }

    void EnemyAlart()
    {
        if (gameObject.name == "BluePlayer(Clone)")
        {
            arrow = GameObject.FindWithTag("RedArrow");
            if (gameObject.activeInHierarchy == false)
            {
                arrow.SetActive(true);
                Invoke("EnableAlart", 5.0f);
            }
        }
        if (gameObject.name == "RedPlayer(Clone)")
        {
            arrow = GameObject.FindWithTag("BlueArrow");
            if (gameObject.activeInHierarchy == false)
            {
                arrow.SetActive(true);
                Invoke("EnableAlart", 5.0f);
            }
        }
    }

    void EnableAlart()
    {
        arrow.SetActive(false);
    }
}
