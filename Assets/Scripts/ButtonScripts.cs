using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    public void startTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void startLevel(int number)
    {
        switch (number)
        {
            case 1:
                SceneManager.LoadScene("Level 1");
                break;
        }
    }
}
