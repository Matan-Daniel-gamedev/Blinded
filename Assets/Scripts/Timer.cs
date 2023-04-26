using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timerDuration = 5;
    private float tmpTime;
    public bool timerIsRunning = false;

    private LightsScript lightsScript;
    private TextMeshProUGUI timerText;

    private RawImage unlit;
    private RawImage lit;
    private void Start()
    {
        tmpTime = timerDuration;

        // Starts the timer automatically
        timerIsRunning = true;
        lightsScript = gameObject.GetComponent<LightsScript>();
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();

        unlit = GameObject.Find("lightbulb_unlit").GetComponent<RawImage>();
        lit = GameObject.Find("lightbulb_lit").GetComponent<RawImage>();
    }
    void Update()
    {
        if (timerIsRunning)
        {
            timerText.text = Math.Ceiling(tmpTime).ToString();
            bool timerRunning = tmpTime > 0;
            if (timerRunning)
            {
                tmpTime -= Time.deltaTime;
            }
            else
            {
                lightsScript.areLightsOn = true;
                tmpTime = 0;
                timerIsRunning = false;

                unlit.enabled = true;
                lit.enabled = false;
            }
        }
    }

    public void startCountdown()
    {
        lightsScript.areLightsOn = false;
        tmpTime = timerDuration;
        timerIsRunning = true;

        unlit.enabled = false;
        lit.enabled = true;
    }
}
