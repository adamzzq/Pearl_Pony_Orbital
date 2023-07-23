using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    public PowerupEffect powerupEffect;
    public AudioSource pickupSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            pickupSound.Play();
            powerupEffect.apply(collision.gameObject);
        }
    }
}
