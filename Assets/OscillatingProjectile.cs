using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates a script for a saw to move backwards and forwards, damaging the player when in contact

public class OscillatingProjectile : MonoBehaviour
{
    private Vector3 pos1 = new Vector3(16,-2.5f,0);
    private Vector3 pos2 = new Vector3(24,-2.5f,0);
    public float speed = 1.0f;

    // Update is called once per frame, this changes saw vector in relation to two given positions
    void Update()
    {
        transform.position = Vector3.Lerp (pos1, pos2, Mathf.PingPong(Time.time*speed, 1.0f));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().PlayerDamage(10);
        }
    }
}
