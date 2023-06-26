using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : NetworkBehaviour
{
    public Animator animator;
    public HealthBar healthBar;

    public float maxHealth;

    [SyncVar]
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    [Command(requiresAuthority = false)]
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        
        //animator trigger hurt;

        if (currentHealth <= 0) 
        {
            Die();
        }
    }

    [Command(requiresAuthority = false)]
    void Die()
    {
        //play dead animation
        Debug.Log("GG");

        //disable
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        this.enabled = false;
    }
}
