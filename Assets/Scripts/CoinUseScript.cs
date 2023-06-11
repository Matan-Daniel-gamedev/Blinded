using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUseScript : MonoBehaviour
{
    public GameObject gameController;
    private coinsUpdation coinsUpdationScript;
    public int hintCost = 2;
    Timer Timer;
    private bool hintUsed = false;
    public Button hintButton;

    private void Start()
    {
        coinsUpdationScript = gameController.GetComponent<coinsUpdation>();
        Timer = GameObject.Find("GameController").GetComponent<Timer>();
        //UpdateButtonInteractability();
    }

    private void Update()
    {
        UpdateButtonInteractability();
    }

    private void UpdateButtonInteractability()
    {
        //Debug.Log(!hintUsed + ", " + (coinsUpdationScript.getCoins() >= hintCost)+", " + Timer.areLightOn());

        hintButton.interactable = 
            !hintUsed 
            && coinsUpdationScript.getCoins() >= hintCost 
            && Timer.areLightOn();
    }

    public void hintUsedFalse()
    {
        this.hintUsed = false;
    }

    public void HintUse()
    {
        if (!hintUsed && coinsUpdationScript.getCoins() >= hintCost && Timer.areLightOn())
        {
            coinsUpdationScript.UseCoins(hintCost);
            hintUsed = true;
            Timer.startCountdown(5);
        }
        else
        {
            if (!Timer.areLightOn())
            {
                Debug.Log("Can't use hint when lights are on");
            }
            else if (hintUsed)
            {
                Debug.Log("Hint already used");
            }
            else
            {
                Debug.Log("Not enough coins!");
            }
        }
    }
}
