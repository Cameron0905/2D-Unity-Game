using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Deals with movement and states of Grunt enemy character

public class GruntScript : MonoBehaviour
{

    // Initialises necessary states for enemy

    public int maxHealth = 50;
    public int health;
    public float attackRange;
    private Animator animator;
    private Rigidbody2D b;
    private Transform player;
    private string currentAnim;
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
        if (inRange == true)
        {
            if (!nowAttacking  && timeSinceLastReset > 1.1f)
            {
                nowAttacking = true;
                timeSinceLastReset = 0;
                ChangeAnimationState("Attack1");
                Collider2D[] dmgdPlayers = Physics2D.OverlapCircleAll(attackPosition.position, 1.2f, playerHitbox);
                StartCoroutine(inflictDmg(dmgdPlayers, 5));
                Invoke("AttackReset", 1.5f);
            }
        }
    }

    // Deals with more complex enemy behaviour, including following player and look position
    public void EnemyBehaviour()
    {
        Vector3 centrePosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        float distanceToPlayer = Vector2.Distance(player.position, centrePosition);
        if (distanceToPlayer >= attackRange)
        {
            inRange = false;
            transform.position = Vector2.MoveTowards(transform.position, player.position, 2f * Time.deltaTime);
            ChangeAnimationState("Run");
        }
        if (distanceToPlayer <= attackRange)
        {   
            inRange = true;
        }
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(2.5f, 2.5f);
        }
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector2(-2.5f, 2.5f);
        }
    }

    // Damages player character
    IEnumerator inflictDmg(Collider2D[] dmgdPlayers, int dmg)
    {
        yield return new WaitForSeconds(0.8f);
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
            ChangeAnimationState("Idle");
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
            ChangeAnimationState("Take Hit");
            SoundManagerScript.PlaySound("enemySound");
            Invoke("DamageReset", 0.10f);
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

    //Deals with enemy death
    void Death()
    {
        ChangeAnimationState("Death");
        dead = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        b.constraints = RigidbodyConstraints2D.FreezePositionY;
        this.enabled = false;
    }

    // Back-end code to show attack range
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireSphere(attackPosition.position, 1.2f);
    }
}
