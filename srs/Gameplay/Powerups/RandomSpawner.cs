using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : NetworkBehaviour
{
    [Header ("Powerup Prefabs")]
    public List<GameObject> objectToSpawn = new List<GameObject>();

    [Header("Spawn Time Setting")]
    public float timeToSpawn;
    private float currentTimeToSpawn;

    [Header("Randomise Setting")]
    public bool isRandom;
    public List<GameObject> spawnPoints = new List<GameObject>();

    [ServerCallback]
    void Start()
    {
        if (!isServer) { return; }
        currentTimeToSpawn = timeToSpawn;
    }

    [ServerCallback]
    void Update()
    {
        if(!isServer) { return; }
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if (currentTimeToSpawn > 0)
        {
            currentTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            SpawnObject();
            currentTimeToSpawn = timeToSpawn;
        }
    }

    //[ClientRpc]
    public void SpawnObject()
    {
        int index = isRandom ? Random.Range(0, objectToSpawn.Count) : 0;
        int spawnPointIndex = isRandom ? Random.Range(0, spawnPoints.Count) : 0;

        if (objectToSpawn.Count > 0)
        {
            GameObject buffs = Instantiate(objectToSpawn[index], spawnPoints[spawnPointIndex].transform.position, Quaternion.identity);
            NetworkServer.Spawn(buffs);
        }
    }
}
