using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public static List<GameObject> Players;
    
    private static CameraBehaviour mainCamera;

    void Awake()
    {
        Players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        mainCamera = GameObject.Find("Main Camera").GetComponent<CameraBehaviour>();
    }
    public static void KillPlayer(GameObject player)
    {
        if(Players.Count == 1)
        {
            SceneManager.LoadScene("GameOver");
        } else
        {
            mainCamera.Targets.Remove(player);
            Players.Remove(player);
            Destroy(player);
        }
    }
}
