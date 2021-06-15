using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barradevida : MonoBehaviour
{
    [SerializeField]
    Color Low;
    [SerializeField]
    Color High;
    [SerializeField]
    Image aa;
    [SerializeField]
    Sprite[] sprites;
    
    public void SetHealth2(float currhealt, float maxHealt)
    {
        aa.sprite = sprites[Convert.ToInt32(maxHealt-currhealt)];
        aa.color = Color.Lerp(Low,High,(currhealt/maxHealt)); 
    }
}
