using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void SelecaoFase(int fase)
    {
        if(fase >= PlayerPrefs.GetInt("FaseAtual"))
        {
            SceneManager.LoadScene(fase);
        } else
        {
            return;
        }
    }
}
