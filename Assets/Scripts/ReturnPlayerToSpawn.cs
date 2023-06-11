using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPlayerToSpawn : MonoBehaviour
{
    GameObject Spawn;
    Checkpoint[] checkpoints;
    Timer Timer;
    public GameObject CoinUse;
    CoinUseScript coinUseScript;
    CameraZoom cameraZoom;

    public float timerDuration = 5f;

    private void Start()
    {
        Spawn = GameObject.FindGameObjectWithTag("Spawn");
        checkpoints = GameObject.FindObjectsOfType<Checkpoint>();
        Timer = GameObject.Find("GameController").GetComponent<Timer>();

        coinUseScript = CoinUse.GetComponent<CoinUseScript>();
        cameraZoom = Camera.main.GetComponent<CameraZoom>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.transform.position = Spawn.transform.position;
            Timer.startCountdown(timerDuration);

            foreach (Checkpoint curr in checkpoints)
            {
                curr.wasTriggered = false;
            }

            coinUseScript.hintUsedFalse();

            // Trigger the camera zoom
            cameraZoom.TriggerZoom();
        }
    }
}
