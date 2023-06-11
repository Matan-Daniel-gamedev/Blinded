using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class coinsUpdation : MonoBehaviour
{
    public Text coinsText;
    private static int coins = 5;

    private void Start()
    {
        // Get the currently active scene
        Scene currentScene = SceneManager.GetActiveScene();
        // Check if the current scene name matches the target scene name
        if (currentScene.name == "Level0")
        {
            coins = 5;
        }
        UpdateCoinText();
    }

    public void AddCoin()
    {
        coins++;
        UpdateCoinText();
    }

    public void UseCoins(int amount)
    {
        coins -= amount;
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        coinsText.text = "Coins: " + coins.ToString();
    }

    public int getCoins()
    {
        return coins;
    }
}
