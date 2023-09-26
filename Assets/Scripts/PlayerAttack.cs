using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAttack : MonoBehaviourPunCallbacks
{
    public GameObject muzzle;
    public GameObject nomalBullet;

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
