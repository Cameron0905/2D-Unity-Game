using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Responsible for destroying heart on collision and displaying floating text

public class HeartScript : MonoBehaviour
{
    public GameObject floatingText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SoundManagerScript.PlaySound("itemSound");
            Instantiate(floatingText, transform.position + transform.up * 0.7f, Quaternion.identity);
            Invoke("DisableHeart", 0.1f);
        }
    }

    void DisableHeart()
    {
        gameObject.SetActive(false);
        this.enabled = false;
    }
}
