using Mirror;
using System.Collections;
using System.Collections.Generic;
using Telepathy;
using UnityEngine;

public class PlayerStats : NetworkBehaviour
{
    public Animator animator;
    public HealthBar healthBar;
    public AudioSource deathSound;

    // Respawn
    private Vector2 startPos;
    private SpriteRenderer playerSprite;
    public GameObject spirit;
    public GameObject healthbarRect;

    public float maxHealth;

    private int opponentPlayer;

    [SyncVar]
    public float currentHealth;

    private void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        startPos = transform.position;
    }

    
    public void HurtAnimation()
    {
        //animator trigger hurt;
        animator.SetTrigger("foxHurt");
    }

    public void TakeDamage(float damage)
    {
        if (isServer)
        {
            RpcTakeDamage(damage);
            opponentPlayer = 2;
            Debug.Log("My opponent is client, I am Server");
        }
        else
        {
            CmdTakeDamage(damage);
            opponentPlayer = 1;
            Debug.Log("My opponent is Server, I am Client");
        }
    }

    [Command(requiresAuthority = false)]
    void CmdTakeDamage(float damage)
    {
        RpcTakeDamage(damage);
    }

    [ClientRpc]
    public void RpcTakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0) 
        {
            Die();
        }
    }

    void Die()
    {
        deathSound.Play();

        
        if (opponentPlayer == 2)
        {
            ScoreManager.instance.Player2AddPoint(); Debug.Log("player 1 died");
        }
        else if (opponentPlayer == 1)
        { 
            ScoreManager.instance.Player1AddPoint(); Debug.Log("player 2 died");
        }
        StartCoroutine(Respawn(2.0f)); Debug.Log("GG");
    }

    IEnumerator Respawn(float freezingTime)
    {
        playerSwitch(false);
        yield return new WaitForSeconds(freezingTime); Debug.Log("finish waiting");

        // refill health to max
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        playerSwitch(true);
        transform.position = startPos; // teleport to start postion 
        Debug.Log("teleported");
    }

    private void playerSwitch(bool status)
    {
        GetComponent<BoxCollider2D>().enabled = status;
        GetComponent<CircleCollider2D>().enabled = status;
        playerSprite.enabled = status;
        spirit.GetComponent<SpriteRenderer>().enabled = status;
        healthbarRect.SetActive(status);
        //gameObject.SetActive(status); Debug.Log($"gameobj set as {status}");
    }
    private void Update()
    {
        if (SoundManager.Instance.GameOver)
        {
            Destroy(gameObject);
        }
    }
    /*void selfKill()
    {
        currentHealth = 0f;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void selfHurt()
    {
        currentHealth -= 1f;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) { selfKill(); }
        if (Input.GetKeyDown(KeyCode.U)) { selfHurt(); }
    }*/
}
