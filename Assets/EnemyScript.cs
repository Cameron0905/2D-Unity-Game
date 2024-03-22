using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Deals with movement and states of Overworld enemy character

public class EnemyScript : MonoBehaviour
{

    // Initialises necessary states for enemy

    public int maxHealth = 60;
    public int health;
    public float attackRange;
    public float lineOfSight;
    private Animator animator;
    private Rigidbody2D b;
    private Transform player;
    private string currentAnim;
    public bool playerFound = false;
    public bool inRange = false;
    public bool nowAttacking = false;
    public bool nowDamaged = false;
    public bool dead = false;

    private float timeSinceLastReset;

    public Transform attackPosition;
    public LayerMask playerHitbox;
    
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
        Animations();
        EnemyBehaviour();
        timeSinceLastReset += Time.deltaTime;
    }

    void ChangeAnimationState(string newAnim)
    {
        if (currentAnim == newAnim) return;
        animator.Play(newAnim);
        currentAnim = newAnim;
    }

    // Deals with simple animations for the enemy, also ensures player can stunlock enemy without being instantly damaged
    void Animations()
    {
        if (!nowDamaged && !dead && !playerFound)
        {
            ChangeAnimationState("HeavyBandit_Idle");
        }
        if (inRange == true)
        {
            if (!nowAttacking && timeSinceLastReset > 1.4f)
            {
                nowAttacking = true;
                timeSinceLastReset = 0;
                ChangeAnimationState("HeavyBandit_Attack");
                Collider2D[] dmgdPlayers = Physics2D.OverlapCircleAll(attackPosition.position, 0.5f, playerHitbox);
                StartCoroutine(inflictDmg(dmgdPlayers, 15));
                Invoke("AttackReset", 1f);
            }
        }
    }

    // Deals with more complex enemy behaviour, including Following player when in LoS, states relating to this and damaging the player, as well as look position
    public void EnemyBehaviour()
    {
        Vector3 centrePosition = new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z);
        float distanceToPlayer = Vector2.Distance(player.position, centrePosition);
        if (distanceToPlayer >= attackRange && distanceToPlayer < lineOfSight)
        {
            playerFound = true;
            inRange = false;
            transform.position = Vector2.MoveTowards(transform.position, player.position, 2f * Time.deltaTime);
            ChangeAnimationState("HeavyBandit_Run");
        }
        if (distanceToPlayer <= attackRange)
        {   
            inRange = true;
        }
        if (distanceToPlayer > lineOfSight)
        {
            playerFound = false;
        }
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    // Damages player character
    IEnumerator inflictDmg(Collider2D[] dmgdPlayers, int dmg)
    {
        yield return new WaitForSeconds(0.5f);
        SoundManagerScript.PlaySound("attackSound");
        foreach(Collider2D dmgdPlayer in dmgdPlayers)
        {
            dmgdPlayer.GetComponent<PlayerHealth>().PlayerDamage(dmg);
        }
    }

    // Attack buffer for balancing/animation reasons
    void AttackReset()
    {
        if (!dead)
        {
            ChangeAnimationState("HeavyBandit_Idle");
        }
        nowAttacking = false;
    }

    // Responsible for enemy taking damage
    public void EnemyDamage(int dmg)
    {
        if (!nowDamaged)
        {
            health -= dmg;
            nowDamaged = true;
            ChangeAnimationState("HeavyBandit_Hurt");
            SoundManagerScript.PlaySound("enemySound");
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

    //Deals with enemy death and loot dropped

    public GameObject ironKey;

    void Death()
    {
        ChangeAnimationState("HeavyBandit_Death");
        dead = true;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        b.constraints = RigidbodyConstraints2D.FreezePositionY;
        Instantiate(ironKey, transform.position + transform.up * 0.7f, Quaternion.identity);
        this.enabled = false;
    }

    // Back-end code to show attack range and LoS of enemy attacks
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }
}
