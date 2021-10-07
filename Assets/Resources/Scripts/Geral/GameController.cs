using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{  
    [SerializeField]
    List<GameObject> players;
    
    public void KillPlayer(GameObject player)
    {
        if (players.Count <= 1)
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/GameOver"));
        } else
        {
            players.Remove(player);
            GameObject.FindObjectOfType<Camera>().GetComponent<CameraBehaviour>().Targets.Remove(player);
            Destroy(player.transform.parent.gameObject);
        }
    }
}