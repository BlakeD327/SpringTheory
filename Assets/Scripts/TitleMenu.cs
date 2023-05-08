using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleMenu : MonoBehaviour
{   
    public string LevelName;

    void Start()
    {

    }

    void Update() 
    {

    }

    public void StartButton()
    {
        SceneManager.LoadScene(LevelName);
    }

    public void OptionsMenu()
    {

    }

    public void OptionsClose() 
    {

    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(LevelName);
    }
}
