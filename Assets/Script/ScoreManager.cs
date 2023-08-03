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
        AuxPlayer1AddPoint();
    }

    [ClientRpc]
    public void AuxPlayer1AddPoint() { player1ScoreText.text = "PLAYER 1: " + player1Score.ToString(); }

    //[Command(requiresAuthority = false)]
    [ClientRpc]
    public void Player2AddPoint()
    {
        player2Score += 1;
        player2ScoreText.text = "PLAYER 2: " + player2Score.ToString();
    }

    public void AddPointTo(int player)
    {
        if (player == 1) { Player1AddPoint(); }
        else if (player == 2)  { Player2AddPoint(); }
    }
}
/*
public class ScoreManager : NetworkBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;

    [SyncVar] public int player1Score = 0;
    [SyncVar] public int player2Score = 0;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void Player1AddPoint()
    {
        player1Score += 1;Debug.Log("player 1 score added by 1, now is: " + player1Score);
        player1ScoreText.text = "PLAYER 1: " + player1Score.ToString();
    }

    public void Player2AddPoint()
    {
        player2Score += 1; Debug.Log("player 2 score added by 1, now is: " + player2Score);
        player2ScoreText.text = "PLAYER 2: " + player2Score.ToString();
    }

    public void AddPointTo(int player)
    {
        if (player == 1) { Player1AddPoint(); }
        else if (player == 2) { Player2AddPoint(); }
    }
}*/
