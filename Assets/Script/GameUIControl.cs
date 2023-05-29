using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIControl : MonoBehaviour
{
   public void GoLobby()
    {
        //SceneManager.LoadScene("Loading Scene");
        SceneManager.LoadScene("Lobby");
    }

    public void Home()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
