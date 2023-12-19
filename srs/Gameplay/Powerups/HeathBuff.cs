using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Healthbuff")]
public class HeathBuff : PowerupEffect
{
    public int amount;
    public override void apply(GameObject target)
    {
        //get current health
        float currentHealth = target.GetComponent<PlayerStats>().currentHealth;
        currentHealth += amount;
        //restore health on the healthbar
        target.GetComponent<PlayerStats>().healthBar.SetHealth(currentHealth);
    }
}
