using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource ButtonClickSound;
    public AudioSource PlayerSelectSound;
    public AudioSource BGMusic;
    public AudioSource countDownSound;
    public AudioSource gameoverSound;

    public bool GameOver = false;

    // Singleton instance of the SoundManager.
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            // If the instance is null, find it in the scene.
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }
            return instance;
        }
    }

    void Awake()
    {
        ButtonClickSound = GameObject.Find("Button Click Sound").GetComponent<AudioSource>();
        PlayerSelectSound = GameObject.Find("Player Select Sound").GetComponent<AudioSource>();

        BGMusic = GameObject.Find("BG Music").GetComponent<AudioSource>();
        countDownSound = GameObject.Find("Count Down Sound").GetComponent<AudioSource>();
        gameoverSound = GameObject.Find("Game Over Music").GetComponent<AudioSource>();
    }

    public void PlayButtonClick()
    {
        ButtonClickSound.Play();
    }
    public void PlaySelectSound()
    {
        PlayerSelectSound.Play();
    }

    public bool DrumSound()
    {
        countDownSound.Play();
        return false;
    }

    public bool GameOverSound()
    {
        countDownSound.Stop();
        BGMusic.Stop();
        gameoverSound.Play();
        return false;
    }

}
