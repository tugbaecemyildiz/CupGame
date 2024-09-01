using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GameOne()
    {
        SceneManager.LoadScene(1);
    }

    public void GameTwo()
    {
        SceneManager.LoadScene(2);
    }
    public void GameThree()
    {
        SceneManager.LoadScene(3);
    }
    public void BackMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
