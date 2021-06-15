using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
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
        player.Move(Input.GetAxis("AxisJoyStick"));
        player.Jump(Input.GetKeyDown(KeyCode.JoystickButton0));
        player.Attack(Input.GetKeyDown(KeyCode.JoystickButton3));
    }
}
