using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timerDuration = 100f;
    private float tmpTime;
    public bool timerIsRunning = false;

    const int infTime = -1;
    const char infinity = '\u221E';
    //float timerDuration = infinity;


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
            if (tmpTime > 100)
            {
                timerText.text = infinity.ToString();
            }
            else
            {
                timerText.text = Math.Ceiling(tmpTime).ToString();
            }
            bool timerRunning = tmpTime > 0;
            if (timerRunning)
            {
                tmpTime -= Time.deltaTime;
            }
            else
            {
                turnOnBlindMode();
            }
        }
    }

    public void startCountdown(float duration)
    {
        if(duration == infTime)
        {
            turnOnBlindCountdown(duration);
            timerIsRunning = false;
            timerText.text = infinity.ToString();
        }
        else
        {
            turnOnBlindCountdown(duration);
        }
        
    }

    public void turnOnBlindCountdown(float duration)
    {
        timerDuration = duration;
        lightsScript.areLightsOn = false;
        tmpTime = timerDuration;
        timerIsRunning = true;


        unlit.enabled = false;
        lit.enabled = true;
    }

    public void turnOffBlindMode()
    {
        startCountdown(infTime);
    }

    public void turnOnBlindMode()
    {
        lightsScript.areLightsOn = true;
        tmpTime = 0;
        timerIsRunning = false;
        timerText.text = infinity.ToString();
        unlit.enabled = true;
        lit.enabled = false;
    }

    public bool areLightOn()
    {
        return lightsScript.areLightsOn;
    }
}
