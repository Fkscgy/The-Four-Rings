using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public int tipo{get;set;}
    IPlayer player;
    void Awake()
    {
        switch(tipo)
        {
            case 1:
            transform.Find("Alysa").gameObject.SetActive(true);
            player = transform.Find("Alysa").GetComponent<IPlayer>();
            break;
            case 2:
            transform.Find("Gasper").gameObject.SetActive(true);
            player = transform.Find("Gasper").GetComponent<IPlayer>();
            break;
            // case 3:
            // transform.Find("Alysa").gameObject.SetActive(true);
            // break;
            // case 4:
            // transform.Find("Alysa").gameObject.SetActive(true);
            // break;
            default:
            transform.Find("Alysa").gameObject.SetActive(true);
            player = transform.Find("Alysa").GetComponent<IPlayer>();
            break;
        }
    }
    void Update()
    {
        player.Move(Input.GetAxis("AxisTeclado"));
        player.Jump(Input.GetKeyDown(KeyCode.Space));
    }
}
