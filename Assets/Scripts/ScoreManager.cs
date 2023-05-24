using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
