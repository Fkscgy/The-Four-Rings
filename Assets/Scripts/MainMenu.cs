using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static int p1,p2 = 0;

    public void LoadGameScene(string index)
    {
        SceneManager.LoadScene(index);
    }
    public void QuitGame()
    {
        // Application.Quit();
    }
    public void SelectPlayer1(int charIndex)
    {
        p1 = charIndex;
    }
    public void SelectPlayer2(int charIndex)
    {
        p2= charIndex;
    }
}