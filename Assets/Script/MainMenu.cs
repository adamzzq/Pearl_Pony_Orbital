using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Gameplay");
        
        
    }

    public void QuitGame()
    {
        Debug.Log("Quit"); //check the functionality of the button
        Application.Quit();
    }
   
}
