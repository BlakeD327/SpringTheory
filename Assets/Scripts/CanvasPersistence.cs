using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasPersistence : MonoBehaviour
{
    private void Awake()
    {   
        // Check if the current scene is the game over screen
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("GameOver"); // hit sound
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}