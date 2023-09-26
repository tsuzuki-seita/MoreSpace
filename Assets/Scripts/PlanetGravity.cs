using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
	GameObject planet;       // 引力の発生する星
	public float accelerationScale; // 加速度の大きさ
	public Rigidbody rb;

	void Start()
	{
		planet = GameObject.Find("Stage");
	}

	void FixedUpdate()
	{
		// 星に向かう向きの取得
		var direction = planet.transform.position - transform.position;
		direction.Normalize();

		// 加速度与える
		rb.AddForce(accelerationScale * direction, ForceMode.Acceleration);
	}
}
