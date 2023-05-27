using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Animator animator;
    public HealthBar healthBar;

    public int maxHealth;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        
        //animator trigger hurt;

        if (currentHealth <= 0) 
        {
            Die();
        }
    }

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
