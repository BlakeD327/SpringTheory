using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float currentTime = 0;
    bool timerActive  = true;
    public TMP_Text timeText;
    
    // Start is called before the first frame update
    void Start()
    {
        timerActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            currentTime += Time.deltaTime;
        }
        DisplayTime();
    }

    void DisplayTime ()
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        float milliseconds = Mathf.FloorToInt((currentTime * 1000) % 1000);
        timeText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds / 10);

    }

    // public void StartTimer()
    // {
    //     timerActive = true;
    // }

    // public void StopTimer()
    // {
    //     timerActive = false;
    // }
}
