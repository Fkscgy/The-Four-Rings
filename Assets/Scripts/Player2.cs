using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public static int tipo{get;set;}
    [SerializeField]
    GameObject[] chars;
    IPlayer player;
    void Awake()
    {
        chars[tipo].gameObject.SetActive(true);
        player = chars[tipo].GetComponent<IPlayer>();
    }
    void Update()
    {
        player.Move(Input.GetAxis("AxisTeclado"));
        player.Jump(Input.GetKeyDown(KeyCode.Space));
        player.Attack(Input.GetKeyDown(KeyCode.Q));
    }
}
