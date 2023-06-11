using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinCollision : MonoBehaviour
{
    public GameObject gameController;
    private coinsUpdation coinsUpdationScript;

    private void Start()
    {
        coinsUpdationScript = gameController.GetComponent<coinsUpdation>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            coinsUpdationScript.AddCoin();
            Destroy(gameObject);
        }
    }
}
