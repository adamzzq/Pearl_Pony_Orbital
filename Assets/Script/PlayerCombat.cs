using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCombat : NetworkBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public float attackSpeed = 2f;
    public int attackDamage = 1;
    float nextAttackTime = 0f;
    void Update()
    {
        if (!isLocalPlayer) return;
        //regulate attack speed
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                //transform.Translate(-0.437f, 0.509f, 0f); Debug.Log("moved back");
                nextAttackTime = Time.time + (1f / attackSpeed);
            }
        }
    }

    void Attack()
    {
        //play attack animation
        animator.SetTrigger("Attack");
        //transform.Translate(0.437f, -0.509f, 0f); Debug.Log("moved");
        //detect the hit
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        //deal damage
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<PlayerStats>().TakeDamage(attackDamage);
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint.position == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}