using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Deals with movement and states of player character

public class Movement : MonoBehaviour
{

    // Initialises necessary states for player

    private Rigidbody2D b;
    private Animator animator;
    private string currentAnim;
    private float xVel;

    [SerializeField]
    float speed = 0.20f;
    [SerializeField]
    float jump = 5f;
    [SerializeField]
    float lightAttackAoe = 0.4f;
    [SerializeField]
    float heavyAttackAoe = 0.3f;

    public Transform attackPosition;
    public LayerMask enemyHitbox;

    public bool grounded = false;
    public bool inSnow = false;
    public bool attackingLight = false;
    public bool attackingHeavy = false;
    public bool nowAttacking = false;
    public bool isDead = false;

    private float timeSinceLastSound;

    // Start is called before the first frame update
    void Start()
    {
    b = GetComponent<Rigidbody2D>();
    animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    // Runs 50 times per second, involves directions, animations and whether player can move
    void FixedUpdate()
    {
        if (gameObject.GetComponent<PlayerHealth>().dead == false)
        {
            Directions();
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += move * Time.deltaTime * speed;
        }
        Animations();
        States();
        isDead = gameObject.GetComponent<PlayerHealth>().dead;
    }

    // Deals with user inputs for player movement

    void Directions()
    {
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed, 0f), ForceMode2D.Impulse);
            transform.localScale = new Vector2(-1, 1);
            timeSinceLastSound += Time.deltaTime;
            if (timeSinceLastSound > 0.4  && grounded == true)
            {
                timeSinceLastSound = 0;
                SoundManagerScript.PlaySound("walkSound");
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0f), ForceMode2D.Impulse);
            transform.localScale = new Vector2(1, 1);
            timeSinceLastSound += Time.deltaTime;
            if (timeSinceLastSound > 0.4 && grounded == true)
            {
                timeSinceLastSound = 0;
                SoundManagerScript.PlaySound("walkSound");
            }
        }
        if (Input.GetKey(KeyCode.Space) && grounded == true)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
            SoundManagerScript.PlaySound("jumpSound");
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            attackingLight = true;
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            attackingHeavy = true;
        }
    }

    // Changes player mass depending on their state

    void States()
    {
        if (inSnow == true)
        {
            b.mass = 3;
        }
        if (inSnow == false)
        {
            b.mass = 1;
        }
    }


    void ChangeAnimationState(string newAnim)
    {
        if (currentAnim == newAnim) return;
        animator.Play(newAnim);
        currentAnim = newAnim;
    }

    // Deals with animations for the player

    void Animations()
    {
        xVel = Input.GetAxisRaw("Horizontal");

        if (xVel == 0 && grounded == true && nowAttacking == false && isDead == false)
        {
            ChangeAnimationState("Idle");
        }
        if (grounded == false)
        {
            ChangeAnimationState("Fall");
        }
        if (xVel != 0 && grounded == true && isDead == false)
        {
            ChangeAnimationState("Run");
        }
        if (attackingLight == true)
        {
            attackingLight = false;
            if (!nowAttacking)
            {
                nowAttacking = true;
                ChangeAnimationState("Dash-Attack");
                SoundManagerScript.PlaySound("attackSound");
                Collider2D[] dmgdEnemies = Physics2D.OverlapCircleAll(attackPosition.position, lightAttackAoe, enemyHitbox);
                inflictDmg(dmgdEnemies, 10);
                Invoke ("AttackReset", 0.25f);
            }
        }
        if (attackingHeavy == true)
        {
            attackingHeavy = false;
            if (!nowAttacking)
            {
                nowAttacking = true;
                ChangeAnimationState("Attack");
                SoundManagerScript.PlaySound("attackSound");
                Collider2D[] dmgdEnemies = Physics2D.OverlapCircleAll(attackPosition.position, heavyAttackAoe, enemyHitbox);
                inflictDmg(dmgdEnemies, 30);
                Invoke("AttackReset", 0.7f);
            }
        }
        if (isDead == true)
        {
            ChangeAnimationState("Death");
            Invoke("BackToMenu", 2f);
        }
        
    }

    void AttackReset()
    {
        nowAttacking = false;
    }

    // Lets player inflict damage to enemies, depending on what scene they are occupying

    void inflictDmg(Collider2D[] dmgdEnemies, int dmg)
    {
        foreach(Collider2D enemy in dmgdEnemies)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                enemy.GetComponent<EnemyScript>().EnemyDamage(dmg);
            }
            if (SceneManager.GetActiveScene().buildIndex == 3)
            { 
                enemy.GetComponent<GruntScript>().EnemyDamage(dmg);
            }
            if (SceneManager.GetActiveScene().buildIndex == 4)
            { 
                enemy.GetComponent<SlimeScript>().EnemyDamage(dmg);
            }
        }
    }

    void BackToMenu()
    {
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadLevel(0);
    }

    // Back-end code to show attack ranges of player attacks

    void OnDrawGizmosSelected()
    {
        
        Gizmos.DrawWireSphere(attackPosition.position, lightAttackAoe);
        Gizmos.DrawWireSphere(attackPosition.position, heavyAttackAoe);
    }
}
