using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinnerDecider : MonoBehaviour
{
    public TMP_Text winner;
    public void decide()
    {
        if (ScoreManager.instance.player1Score > ScoreManager.instance.player2Score)
        {
            winner.text = "PLAYER 1";
            winner.color = Color.red;
        }
        else if (ScoreManager.instance.player1Score < ScoreManager.instance.player2Score)
        {
            winner.text = "PLAYER 2";
            winner.color = Color.blue;
        }
        else
        {
            winner.text = "DRAW, HAPPY ENDING";
            winner.color = Color.green;
        }
    }

}
