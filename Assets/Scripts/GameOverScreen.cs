using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    private void Start()
    {
        // Stop the timer when the GameOver scene loads
        Timer.Instance.StopTimer();
        FindObjectOfType<AudioManager>().Play("GameOver"); // hit sound

    }

    public void RestartButton()
    {
        ScoreManager.Instance.ResetScore();
        Timer.Instance.ResetTimer();
        SceneManager.LoadScene("Level1");
        Timer.Instance.StartTimer();
    }
    public void MainMenuButton()
    {
        ScoreManager.Instance.ResetScore();
        Timer.Instance.ResetTimer();
        SceneManager.LoadScene("Title Screen");
    }
}