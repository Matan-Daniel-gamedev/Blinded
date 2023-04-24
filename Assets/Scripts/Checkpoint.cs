using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Timer Timer;
    public bool wasTriggered = false;
    private void Start()
    {
        Timer = GameObject.Find("GameController").GetComponent<Timer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Checkpoint" && !wasTriggered)
        {
            Timer.startCountdown();
            wasTriggered = true;
        }
    }
}
