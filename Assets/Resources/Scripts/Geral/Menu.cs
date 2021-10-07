using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject creditos;
    void Start()
    {
        Destroy(creditos,3f);
    }
    public void Fase(string fase)
    {
        SceneManager.LoadScene(fase);
    }
}