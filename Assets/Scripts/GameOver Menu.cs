using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverMenu : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void MainMenu()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
