using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    public GameObject prefabObject;  // 配置するPrefabオブジェクト
    public int numberOfObjects = 7;  // 配置するオブジェクトの数
    public float minDistance = 5.0f;  // 最小配置間隔
    public float maxDistance = 20.0f; // 最大配置間隔

    private void Start()
    {
        PlaceObjectsOnSurface();
    }

    private void PlaceObjectsOnSurface()
    {
        // 凹凸のある大きな球体を取得
        GameObject sphere = GameObject.Find("Stage");  // 仮の名前で球体を示しています
        if (sphere == null)
        {
            Debug.LogError("Sphere not found. Make sure to set the correct sphere object name.");
            return;
        }

        for (int i = 0; i < numberOfObjects; i++)
        {
            // 球面上にランダムな位置に配置
            float latitude = Random.Range(0f, Mathf.PI);
            float longitude = Random.Range(0f, Mathf.PI * 2);

            // 設置面に対して垂直な向きに修正
            Vector3 position = new Vector3(Mathf.Sin(latitude) * Mathf.Cos(longitude),
                                          Mathf.Cos(latitude),
                                          Mathf.Sin(latitude) * Mathf.Sin(longitude));

            position *= sphere.transform.localScale.x * 0.5f;  // 球体の半径分スケール

            // オブジェクトの向きを設定
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, position);

            // オブジェクトを配置
            GameObject newObject = Instantiate(prefabObject, position, rotation);
            newObject.transform.SetParent(sphere.transform);

            // オブジェクト間の距離を設定
            float distance = Random.Range(minDistance, maxDistance);
            newObject.transform.Translate(Vector3.forward * distance);
        }
    }
}
