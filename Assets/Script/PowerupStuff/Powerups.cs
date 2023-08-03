using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    public PowerupEffect powerupEffect;
    public AudioSource pickupSound;
    private void Awake()
    {
        pickupSound = GameObject.Find("Health Buff").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pickupSound.Play(); //Debug.Log("Pick up health buff!");
            Destroy(gameObject);
            powerupEffect.apply(collision.gameObject);
        }
    }
}