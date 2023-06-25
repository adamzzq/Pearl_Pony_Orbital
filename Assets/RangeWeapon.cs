using Mirror.Examples.AdditiveScenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RangeWeapon : NetworkBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    public float shootSpeed;
    float nextShootTime;

    [ClientCallback]
    private void Update()
    {
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.J))
        {
            if (Time.time > nextShootTime) 
            {
                nextShootTime = Time.time + (1 / shootSpeed);
                shoot();
            }
        }
    }

    [Command]
    void shoot()
    {
        GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation);
        NetworkServer.Spawn(projectile);
    }
}
