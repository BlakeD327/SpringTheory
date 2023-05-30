using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public static TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void IncrementScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private static void UpdateScoreText()
    {
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            scoreText.text = "Final Score: " + score;
        } else {
            scoreText.text = "Score: " + score;
        }

        // if (scoreText != null)
        // {
        //     scoreText.text = "Score: " + score;
        // }
        
    }

    // Test Code

    public void ResetScore()
    {
        score = 0;
    }

    // Singleton instance
    private static ScoreManager instance;

    public static ScoreManager Instance
    {
        get { return instance; }
    }

    // Other variables and methods

    private void Awake()
    {
        if (instance == null)
        {
            // Set the instance if it doesn't exist
            instance = this;
        }
        else
        {
            // Destroy the duplicate instance
            Destroy(gameObject);
        }
    }
}
