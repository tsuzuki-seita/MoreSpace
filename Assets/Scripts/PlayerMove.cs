using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 50.0f;  // プレイヤーの移動速度
    public float rotationSpeed = 5.0f;  // プレイヤーの回転速度
    public float bonus;

    private bool isGrounded = true;  // 地面に接しているかどうか

    // Start is called before the first frame update
    void Start()
    {
        bonus = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //float eulerAngleZ = transform.eulerAngles.z;
        //var min = -20;
        //var max = 20;

        //var a = eulerAngleZ;
        //a = Mathf.Clamp(eulerAngleZ, min, max);


        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, a);

        // WASDキーの入力を取得
        float moveInputHorizontal = Input.GetAxis("Horizontal");
        float moveInputVertical = Input.GetAxis("Vertical");

        // 移動方向を計算
        Vector3 movement = transform.right * moveInputHorizontal + transform.forward * moveInputVertical;

        // ADキーの入力でプレイヤーの向きを傾ける
        if (moveInputHorizontal != 0)
        {
            // 右に傾ける
            float rotationAngle = moveInputHorizontal > 0 ? 1.0f : -1.0f;
            transform.Rotate(Vector3.up, rotationAngle * rotationSpeed * Time.deltaTime);
        }

        // プレイヤーを移動させる
        //transform.up = new Vector3(0, moveSpeed * Time.deltaTime, 0);
        transform.Translate(movement * moveSpeed * Time.deltaTime * bonus, Space.World);

    }
}
