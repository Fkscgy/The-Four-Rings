using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeFundo : MonoBehaviour
{
    SpriteRenderer sr;
    [SerializeField]
    Sprite sprite;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            sr.sprite = sprite;
        }
    }
}
