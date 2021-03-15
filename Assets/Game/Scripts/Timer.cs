using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private int startTimeInSecs;
    private float timeRemaining;
    private bool timerIsRunning = false;
    private FMODUnity.StudioEventEmitter fmodEvent;
    private bool stopTimer = false;

    public int StartTimeInSecs => startTimeInSecs;

    public int TimeRemainingInSec => Convert.ToInt32(timeRemaining);

    private Text timerDisplay;
    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        timerDisplay = this.gameObject.GetComponent<Text>();
        timeRemaining = startTimeInSecs;
        fmodEvent = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    private void Update()
    {
        if (stopTimer) return;
        
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
                if (timeRemaining <= 5 && fmodEvent.enabled == false)
                {
                    fmodEvent.enabled = true;
                }
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

    public void StopTimer()
    {
        stopTimer = true;
    }
}

public delegate void CountCompletedHandler();