using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneInput : MonoBehaviour
{
    [SerializeField]
    string cena;
    bool isInsideCollider;
    void OnTriggerEnter2D(Collider2D col)
    {
        isInsideCollider = true;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        isInsideCollider = false;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && isInsideCollider)
        {
            SceneManager.LoadScene(cena);
        }
    }
}
