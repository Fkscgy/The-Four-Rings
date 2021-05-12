using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    public Slider slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;
    
    public void SetHealth(float currhealt, float maxHealt)
    {
        slider.gameObject.SetActive(currhealt<maxHealt);
        slider.value = currhealt;
        slider.maxValue = maxHealt;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low,High,slider.normalizedValue);
    }
    // Update is called once per frame
    void Update()
    {
     slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
}
