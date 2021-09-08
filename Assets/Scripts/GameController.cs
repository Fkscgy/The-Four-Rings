using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static List<GameObject> Players;

    public static List<GameObject> Players1 { get => Players;private set => Players = value; }

    void Awake()
    {
        Players1 = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
    }
    public static void KillPlayer(GameObject player)
    {
        if(Players1.Count == 1)
        {
            GameObject.Find("GameOverObject").transform.GetChild(0).gameObject.SetActive(true);
        } else
        {
            GameObject.Find("Main Camera").GetComponent<CameraBehaviour>().Targets.Remove(player);
            Destroy(player);
        }
    }
}