using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueNomalBulletMove : MonoBehaviour
{
    Rigidbody rb;
    GameObject player;
    [SerializeField] float accelerationScale;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("BluePlayer(Clone)");
        player = GameObject.Find("BluePlayer(Clone)");
        rb = GetComponent<Rigidbody>();

        //Vector3 forceDirection = new Vector3(1.0f, 1.0f, 0f);

        // 向きと大きさからSphereに加わる力を計算する
        Vector3 force = player.transform.forward * accelerationScale;

        // 力を加えるメソッド
        // ForceMode.Impulseは撃力
        rb.AddForce(force, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
