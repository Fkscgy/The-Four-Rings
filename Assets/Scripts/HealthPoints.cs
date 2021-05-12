using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Contador : MonoBehaviour
{
    Text score;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
        score.text = "Vida do player : " + PlayerBehaviour.playerHP;
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Vida do Player : " + PlayerBehaviour.playerHP;
    }
}
