using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DirectionMark : MonoBehaviourPunCallbacks
{
    private Transform target;
    bool isFirst;
    [SerializeField, Tooltip("プレイヤーオブジェクト")]
    private Transform player = null;

    // カメラ情報
    [SerializeField, Tooltip("プレイヤーを映すカメラ")]
    private Transform Camera = null;

    // Start is called before the first frame update
    void Start()
    {
        isFirst = true;
    }

    void FirstCheck()
    {
        //if (transform.parent.gameObject.name == "BluePlayer(Clone)")
        //{
        //    target = GameObject.Find("RedPlayer(Clone)").transform;
        //}
        //if (transform.parent.gameObject.name == "RedPlayer(Clone)")
        //{
        //    target = GameObject.Find("BluePlayer(Clone)").transform;
        //}
        if (!photonView.IsMine)
        {
            target = GameObject.FindWithTag("Player").transform;
        }

        isFirst = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFirst)
        {
            FirstCheck();
        }
        Vector3 posi = target.transform.localPosition;

        // プレイヤーからターゲットまでのベクトルを計算
        Vector3 Direction = (posi - player.transform.position).normalized;

        // 求めた方向への回転量を求める
        Quaternion RotationalVolume = Quaternion.LookRotation(Direction, Vector3.up);

        // カメラ情報を元に回転量の補正
        Quaternion CorrectionVolume = Quaternion.FromToRotation(Camera.transform.forward, Vector3.forward);

        // 向きを反映
        transform.rotation = RotationalVolume * CorrectionVolume;
        //this.transform.LookAt(target);
    }
}
