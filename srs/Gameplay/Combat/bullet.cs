using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class bullet : NetworkBehaviour
{
    public float bulletDamage = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStats>().TakeDamage(bulletDamage);
        }
        Destroy(gameObject);
    }
}
