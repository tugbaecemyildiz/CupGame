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

    public void GameFour()
    {
        SceneManager.LoadScene(4);
    }

    public void GameFive()
    {
        SceneManager.LoadScene(5);
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
