using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class bullet : NetworkBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerStats enemy = GetComponent<PlayerStats>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
