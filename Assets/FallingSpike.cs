using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Activates gravity on hanging spike at a random time, which can damage the player

public class FallingSpike : MonoBehaviour
{
    Rigidbody2D b;
    private bool fall;
    private bool hit;

    // Start is called before the first frame update
    void Start()
    {
        b = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame, activates gravity when random number is equal to five
    void Update()
    {
        RNG();
        if (fall == true)
        {
            b.gravityScale = 1;
        }
        OnHit();
    }

    // Creates RNG which attempts to equate the number generated to '5'
    void RNG()
    {
        int randomNumber = Random.Range(1, 2100);
        if (randomNumber == 5)
        {
            fall = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hit = true;
        }
    }

    // If falling spike collides with player, damage is dealt
    void OnHit()
    {
        if (hit == true)
        {
            gameObject.GetComponent<EdgeCollider2D>().enabled = false;
            hit = false;
            GameObject.Find("Player").GetComponent<PlayerHealth>().PlayerDamage(40);
        }
    }

}
