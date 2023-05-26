using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;

    void Start()
    {
        Vector2 spawnPos = new Vector2(transform.position.x + 3.0f, transform.position.y);
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity);
    }

}
