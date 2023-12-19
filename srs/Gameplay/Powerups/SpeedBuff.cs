using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : MonoBehaviour
{
    
    public AudioSource pickupSound;
    public float multiplier;
    public float duration;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider2D player)
    {
        //apply pickup effect
        //Instantiate(pickupEffect, transform.position, transform.rotation);
        pickupSound.Play();

        //apply speed buff
        SimpleMovement stats= player.GetComponent<SimpleMovement>();
        stats.speed *= multiplier;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        //buff last duration
        yield return new WaitForSeconds(duration);

        //revert the effect
        stats.speed /= multiplier;

        Destroy(gameObject);
    }
}
