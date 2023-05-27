using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab1;
    public GameObject playerPrefab2;
    private GameObject chosenPrefab;
    private float spawnPosOffset = 3.0f;
    void Start()
    {
        chosenPrefab = (PlayerSelector.Instance.playerChosen == playerPrefab1.name) ? 
                       playerPrefab1 : playerPrefab2;
        // Debug.Log(chosenPrefab.name);
        if (chosenPrefab.name == "Rabbit")
        {
            spawnPosOffset -= 6.0f;
        }
        Vector2 spawnPos = new Vector2(transform.position.x + spawnPosOffset, transform.position.y);
        PhotonNetwork.Instantiate(chosenPrefab.name, spawnPos, Quaternion.identity);
    }
}
