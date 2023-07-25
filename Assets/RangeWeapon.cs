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
    private float bulletSpeed = 20f;

    public AudioSource shootingSound;

    private void Update()
    {
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time > nextShootTime) 
            {
                nextShootTime = Time.time + (1 / shootSpeed);
                shootingSound.Play();
                shoot();
            }
        }
    }

    [Command]
    void shoot()
    {
        GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.root.localScale.x, 0) * bulletSpeed;
        NetworkServer.Spawn(projectile);
    }
}
