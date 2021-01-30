using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    void Start()
    {
        Timer timer = GameObject.Find("Timer").GetComponent(typeof(Timer)) as Timer;
        timer.CountCompleted += () =>
        {
            Destroy(gameObject);
        };
    }

    
}
