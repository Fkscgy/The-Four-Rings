using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    int Hp{get;set;}
    int MaxHp{get;set;}
    void PlayerTakeDamage(int dmg);
}
