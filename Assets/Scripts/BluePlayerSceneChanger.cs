using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BluePlayerSceneChanger : MonoBehaviour
{
    public void GetWinnerName(string name)
    {
        ResultManager.winnerName = name;
        SceneManager.LoadScene("ResultScene");  //勝った方をシーン遷移
    }
}
