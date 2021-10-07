using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ganhou : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
    }
}
