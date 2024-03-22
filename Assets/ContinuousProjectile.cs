using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Continuously shoots a projectile, after a delay, which damages the player

public class ContinuousProjectile : MonoBehaviour
{
    public GameObject continuousProjectile;
    Rigidbody2D b;
    public bool hit;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Projectile", 3f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        DestroyProjectile();
        OnHit();
    }

    // Spawns projectile and moves it in a given direction
    void Projectile()
    {
        Instantiate(continuousProjectile, transform.position, Quaternion.identity);
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-5f, 0f), ForceMode2D.Impulse);
    }

    // Destroys projectile after set time
    void DestroyProjectile()
    {
         Destroy(gameObject, 8);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hit = true;
        }
    }

    // When damaging player, projectile is also destroyed
    void OnHit()
    {
        if (hit == true) {
            Destroy(gameObject);
            hit = false;
            GameObject.Find("Player").GetComponent<PlayerHealth>().PlayerDamage(15);
        }
    }
}
