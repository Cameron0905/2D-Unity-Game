using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Deals with movement and states of Slime enemy character

public class SlimeScript : MonoBehaviour
{

    // Initialises necessary states for enemy

    public int maxHealth = 100;
    public int health;
    public float attackRange;
    private Animator animator;
    private Rigidbody2D b;
    private Transform player;
    private string currentAnim;
    public bool nowDamaged = false;
    public bool dead = false;

    private float timeSinceAttackSound;

    public Transform attackPosition;
    public LayerMask playerHitbox;

    public GameObject endScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        animator = gameObject.GetComponent<Animator>();
        b = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyBehaviour();
        timeSinceAttackSound += Time.deltaTime;
    }

    void ChangeAnimationState(string newAnim)
    {
        if (currentAnim == newAnim) return;
        animator.Play(newAnim);
        currentAnim = newAnim;
    }

    // Deals with following player, and sprite direction
    public void EnemyBehaviour()
    {
        Vector3 centrePosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        float distanceToPlayer = Vector2.Distance(player.position, centrePosition);
        Vector3 playerX = new Vector3(player.position.x, transform.position.y, player.position.z);
        if (distanceToPlayer >= attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerX, 2f * Time.deltaTime);
            if (!nowDamaged)
            {
                ChangeAnimationState("Walk");
            }
        }
        if (distanceToPlayer < 2.25f && timeSinceAttackSound > 1.5f)
        {
            timeSinceAttackSound = 0;
            SoundManagerScript.PlaySound("slimeAttackSound");
        }
        
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(10f, 10f);
        }
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector2(-10f, 10f);
        }
    }

    // Responsible for enemy taking damage
    public void EnemyDamage(int dmg)
    {
        if (!nowDamaged)
        {
            health -= dmg;
            nowDamaged = true;
            ChangeAnimationState("Idle");
            Invoke("DamageReset", 0.15f);
        }
    }

    void DamageReset()
    {
        nowDamaged = false;
        if (health <= 0)
        {
            Death();
        }
    }

    //Deals with enemy death, including end screen showing player they completed the game
    void Death()
    {
        ChangeAnimationState("Spin");
        SoundManagerScript.PlaySound("slimeDeathSound");
        dead = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        b.constraints = RigidbodyConstraints2D.FreezePositionY;
        this.enabled = false;
        Invoke("EndGame", 1.35f);
    }

    // Responsible for end screen and pausing the game
    void EndGame()
    {
        endScreen.SetActive(true);
        SoundManagerScript.PlaySound("endSound");
        Time.timeScale = 0f;
    }

    // Back-end code to show attack range
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
