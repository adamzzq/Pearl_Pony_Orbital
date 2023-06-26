using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LocalCameraFollow : NetworkBehaviour
{
    [SerializeField] private CinemachineVirtualCamera localCamera;

    void Start()
    {
        if (isLocalPlayer) 
        { 
            localCamera = CinemachineVirtualCamera.FindObjectOfType<CinemachineVirtualCamera>();
            //localCamera.LookAt = this.gameObject.transform;
            localCamera.Follow = this.gameObject.transform;
        }
    }
}
