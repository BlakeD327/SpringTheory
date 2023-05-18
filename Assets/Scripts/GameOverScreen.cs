using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene("Tutorial1");
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene("Title Screen");
    }
}
