using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoroDaPaixao : MonoBehaviour
{
    bool aaaa = false;
    [SerializeField]
    GameObject prefab;
    float varX;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U) && PlayerOneBehaviour.ultimateIsReady)
        {
            StartCoroutine(example());
        }
    }
    IEnumerator example()
    {
        for(int i =0;i<25;i++)
        {
            varX =Random.Range(-25f,15f);
            Instantiate(prefab,new Vector3(varX,4.4f,0f),Quaternion.Euler(0f,0f,-45f));
            yield return new WaitForSeconds(Random.Range(0.05f,0.1f));
        }
    }
}