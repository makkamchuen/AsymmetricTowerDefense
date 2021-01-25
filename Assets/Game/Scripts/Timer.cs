using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int startTimeInSecs;
    private float timeRemaining;
    private bool timerIsRunning = false;

    private Text timerDisplay;
    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        timerDisplay = this.gameObject.GetComponent<Text>();
        timeRemaining = startTimeInSecs;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                CountCompleted?.Invoke();
            }
        }
    }
    
    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public event CountCompletedHandler CountCompleted;
    
    protected virtual void OnCountCompleted() //protected virtual method
    {
        CountCompleted?.Invoke(); 
    }
}

public delegate void CountCompletedHandler(); 