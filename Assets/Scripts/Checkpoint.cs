using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Timer Timer;
    public bool wasTriggered = false;
    public float timerDuration = 5f;
    private void Start()
    {
        Timer = GameObject.Find("GameController").GetComponent<Timer>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && !wasTriggered)
        {
            //Timer.startCountdown(timerDuration);
            Timer.turnOnBlindMode();
            wasTriggered = true;
        }
    }
}
