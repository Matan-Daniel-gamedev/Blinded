using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPlayerToSpawn : MonoBehaviour
{
    GameObject Spawn;
    Checkpoint checkpoint;
    Timer Timer;
    private void Start()
    {
        Spawn = GameObject.FindGameObjectWithTag("Spawn");
        checkpoint = GameObject.Find("Player").GetComponent<Checkpoint>();
        Timer = GameObject.Find("GameController").GetComponent<Timer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.gameObject.GetComponent<Transform>().position = Spawn.transform.position;
            Timer.startCountdown();
            checkpoint.wasTriggered = false;
        }
    }
}
