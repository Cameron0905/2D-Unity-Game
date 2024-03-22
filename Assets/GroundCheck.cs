using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Checks what is interacting with player feet and changes appropiate states and health of player

public class GroundCheck : MonoBehaviour
{
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Player.GetComponent<Movement>().grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Player.GetComponent<Movement>().grounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Snow") 
        {
            Player.GetComponent<Movement>().inSnow = true;
        }
        if (collision.gameObject.tag == "Spike") 
        {
            InvokeRepeating("SpikeDamage", 0f, 0.3f);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Snow")
        {
            Player.GetComponent<Movement>().inSnow = false;
        }
        if (collision.gameObject.tag == "Spike")
        {
            CancelInvoke();
        }
    }

    void SpikeDamage()
    {
        GameObject.Find("Player").GetComponent<PlayerHealth>().PlayerDamage(10);
    }
}
