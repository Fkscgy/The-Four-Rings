using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour
{
    public string sceneToLoad;
    bool a;
    
    void Update()
    {
        if (a && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        a = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        a = false;
    }
}
