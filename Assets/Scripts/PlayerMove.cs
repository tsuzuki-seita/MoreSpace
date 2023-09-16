using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;  // プレイヤーの移動速度
    public float rotationSpeed = 100.0f;  // プレイヤーの回転速度
    public float jumpForce = 8.0f;  // ジャンプの力

    private bool isGrounded = true;  // 地面に接しているかどうか

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

        // ジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // プレイヤーを移動させる
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    private void Jump()
    {
        // ジャンプ力を加える
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 地面に接触したらジャンプ可能にする
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
