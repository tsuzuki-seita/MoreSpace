using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    GameObject player;   //プレイヤー情報格納用
    private Vector3 offset;      //相対距離取得用

    // Use this for initialization
    void Start()
    {
        player = transform.parent.gameObject;
        //player = GameObject.Find("Player");

        // MainCamera(自分自身)とplayerとの相対距離を求める
        offset = transform.position - player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //新しいトランスフォームの値を代入する
        //transform.position = player.transform.position + offset;

        //FPS視点
        //transform.position = player.transform.position + player.transform.forward * -0.5f + player.transform.up * 1;
        transform.position = player.transform.position + player.transform.forward * -50 + player.transform.up * 20;
    }
}
