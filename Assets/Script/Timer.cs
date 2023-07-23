using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : NetworkBehaviour
{
    public TMP_Text timerText;

    public WinnerDecider winnerDecider;
    public GameOverScreen gameOverScreen;
    public AudioSource gameoverSound;

    [SyncVar]
    public float totalTime = 90;

    void Update()
    {
        if (totalTime > 0)
        {
            totalTime -= Time.deltaTime;
        }
        else
        {
            totalTime = 0;
            gameoverSound.Play();
            winnerDecider.decide();
            gameOverScreen.Setup();
        }

        DisplayTime(totalTime); 
    }

    [ClientCallback]
    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        else if (timeToDisplay > 0)
        {
            timeToDisplay += 1;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
