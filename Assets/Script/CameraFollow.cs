using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform Player;

    private Vector3 tempPos;

    [SerializeField]
    private float minX, minY, maxX, maxY;
    
    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Player == null)
        {
            Start();
            return;
        }

        tempPos = transform.position;
        tempPos.x = Player.position.x;
        tempPos.y = Player.position.y;

        if (tempPos.x < minX)
        {
            tempPos.x = minX;
        }
        if (tempPos.x > maxX)
        {
            tempPos.x = maxX;
        }

        if (tempPos.y < minY)
        {
            tempPos.y = minY;
        }
        if (tempPos.y > maxY)
        {
            tempPos.y = maxY;
        }

        transform.position = tempPos;
        
    }
}
