using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    string Tipo{get;set;}
    bool IsReady{get;set;}
    void PlayerTakeDamage(int dmg);
}