using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanBuff : MonoBehaviour
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
        //apply titan buff
        PlayerCombat dmg = player.GetComponent<PlayerCombat>();
        SpriteRenderer color = player.GetComponent<SpriteRenderer>();

        dmg.attackDamage *= multiplier;
        color.color = Color.red;

        /*player.transform.localScale *= multiplier;

        BoxCollider2D boxSize = player.GetComponent<BoxCollider2D>();
        CircleCollider2D circleSize = player.GetComponent<CircleCollider2D>();
        PlayerStats stats = player.GetComponent<PlayerStats>();
        PlayerCombat dmg = player.GetComponent<PlayerCombat>();

        boxSize.size *= multiplier;
        circleSize.radius *= multiplier;
        dmg.attackDamage *= multiplier;
        stats.currentHealth *= multiplier;
        stats.healthBar.SetHealth(stats.currentHealth);*/

        //disable powerup graphics
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        //buff last duration
        yield return new WaitForSeconds(duration);

        //revert the effect
        dmg.attackDamage /= multiplier;
        color.color = Color.white;
        /*player.transform.localScale /= multiplier;
        boxSize.size /= multiplier;
        circleSize.radius /= multiplier;
        dmg.attackDamage /= multiplier;
        stats.currentHealth /= multiplier;
        stats.healthBar.SetHealth(stats.currentHealth);*/

        Destroy(gameObject);
    }
}
