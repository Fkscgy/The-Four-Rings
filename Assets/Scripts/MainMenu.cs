using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    string fase;
    public void LoadGameScene(string index)
    {
        SceneManager.LoadScene(index);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void SelectPlayer1(int charIndex)
    {
        Player1.tipo = charIndex;
    }
    public void SelectPlayer2(int charIndex)
    {
        Player2.tipo = charIndex;
    }
    public void SelectLevel(string levelIndex)
    {
        fase = levelIndex;
    }
    public void Play()
    {
        SceneManager.LoadScene(fase);
    }
}