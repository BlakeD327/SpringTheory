using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasPersistence : MonoBehaviour
{
    private void Awake()
    {   
        DontDestroyOnLoad(gameObject);
    }
    
    private void Update()
    {
       // Check if the current scene is the game over screen
        if (SceneManager.GetActiveScene().name == "Title Screen")
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }

    }
}