using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    [SerializeField]
    GameObject[] personagens;

    void Start()
    {
        InitializePlayer(MainMenu.p1,MainMenu.p2);
    }
    public void InitializePlayer(int index1, int index2)
    {
        Instantiate(personagens[0]).GetComponent<Player1>().tipo = index1;
        Instantiate(personagens[1]).GetComponent<Player2>().tipo = index2;
    }  
}
public interface IPlayer
{
    void Move(float axis);
    void Jump(bool key);
    void Attack();
    IEnumerator Ultimate();
    void PlayerTakeDamage(float dmg);
}