using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneInput : MonoBehaviour
{
    [SerializeField]
    string cena;
    bool isInsideCollider;
    List<GameObject> players;

    public List<GameObject> Players { get => players; set => players = value; }

    void Start()
    {
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        col.GetComponent<IPlayer>().IsReady = true;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        col.GetComponent<IPlayer>().IsReady = false;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && TestPlayer())
        {
            SceneManager.LoadScene(cena);
        }
    }
    bool TestPlayer()
    {
        foreach (GameObject player in players)
        {
            if(!player.GetComponent<IPlayer>().IsReady)
            {
                return false;
            }
        }
        return true;
    }
}
