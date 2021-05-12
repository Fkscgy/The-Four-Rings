using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToWorld : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadCena", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LoadCena()
    {
        SceneManager.LoadScene("World");
    }
}
