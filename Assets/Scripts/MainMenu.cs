using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public int defaultLives = 3;
    public int defaultScore;
    public int HighScore;

    private void Start()
    {
        PlayerPrefs.SetInt("CurrentLives", defaultLives);
    }
    public void ComenzarJuego()
    {
        SceneManager.LoadScene(0);
    }

    public void RestarGame()
    {
        PlayerPrefs.SetInt("CurrentLives", defaultLives);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
