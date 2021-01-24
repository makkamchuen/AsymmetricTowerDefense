using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
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
