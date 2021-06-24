using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    [SerializeField]
    GameObject[] personagens;
    void Start()
    {
        foreach(var p in personagens)
        {
            Instantiate(p);
        }
    }
}
public interface IPlayer
{
    void Move(float axis);
    void Jump(bool key);
    void Attack(bool key);
    void UsingUltimate(bool key);
    void PlayerTakeDamage(float dmg);
}