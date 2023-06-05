using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPlayerToSpawn : MonoBehaviour
{
    GameObject Spawn;
    Checkpoint[] checkpoints;
    Timer Timer;

    public float timerDuration = 10f;

    private void Start()
    {
        Spawn = GameObject.FindGameObjectWithTag("Spawn");
        
        checkpoints = GameObject.FindObjectsOfType<Checkpoint>();
        Timer = GameObject.Find("GameController").GetComponent<Timer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.gameObject.GetComponent<Transform>().position = Spawn.transform.position;
            //Timer.startCountdown(timerDuration);
            Timer.turnOffBlindMode();
            foreach(Checkpoint curr in checkpoints)
            {
                curr.wasTriggered = false;
            }
        }
    }
}
