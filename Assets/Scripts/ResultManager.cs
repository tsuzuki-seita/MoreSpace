using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    public static string winnerName;
    [SerializeField] Text playerName;
    // Start is called before the first frame update
    void Start()
    {
        playerName.text = winnerName;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
