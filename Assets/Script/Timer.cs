using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Timer : NetworkBehaviour
{
    public TMP_Text timerText;

    public WinnerDecider winnerDecider;
    public GameOverScreen gameOverScreen;
    
    private SoundManager gameSoundManager;
    // flags in Update() to prevent multiple calls to play music
    private bool canPlayMusic = true; 
    private bool canPlayDrum = true;

    [SyncVar]
    public float totalTime = 90;
    
    private void Start()
    {
        gameSoundManager = SoundManager.Instance;
    }

    private void Update()
    {
        totalTime -= Time.deltaTime;
        
        if ((int)totalTime <= 9 && (int)totalTime >= 0 && canPlayDrum)
        {
            canPlayDrum = gameSoundManager.DrumSound();
        }
        else if ((int)totalTime < 0 && canPlayMusic)
        {
            totalTime = 0;
            winnerDecider.decide();
            gameOverScreen.Setup();
            canPlayMusic = gameSoundManager.GameOverSound();
            SoundManager.Instance.GameOver = true; // trigger Destroy(gameObject) in PlayerStats
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
