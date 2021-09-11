using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    [SerializeField]
    List<int> fases;
    void OnLevelWasLoaded(int level)
    {
        if(fases.Contains(level) && level > PlayerPrefs.GetInt("FaseAtual"))
        {
            PlayerPrefs.SetInt("FaseAtual", level);
        }
    }
    public static void KillPlayer(GameObject player)
    {
        if(GameObject.FindGameObjectsWithTag("Player").Length <= 1)
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/GameOver"));
        } else
        {
            GameObject.FindObjectOfType<Camera>().GetComponent<CameraBehaviour>().Targets.Remove(player);

            Destroy(player);
        }
    }
}