using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : NetworkBehaviour
{
    public static ScoreManager instance;

    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;

    [SyncVar]
    public int player1Score = 0;
    [SyncVar]
    public int player2Score = 0;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        player1ScoreText.text = "PLAYER 1: " + player1Score.ToString();
        player2ScoreText.text = "PLAYER 2: " + player2Score.ToString();
    }

    [Command(requiresAuthority = false)]
    public void Player1AddPoint()
    {
        player1Score += 1;
        player1ScoreText.text = "PLAYER 1: " + player1Score.ToString();
    }

    [Command(requiresAuthority = false)]
    public void Player2AddPoint()
    {
        player2Score += 1;
        player2ScoreText.text = "PLAYER 2: " + player2Score.ToString();
    }
}
