using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 0f;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(PlayerPrefs.GetInt("FaseAtual"));
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        }
    }
}
