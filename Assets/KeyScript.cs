using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Responsible for destroying key on collision and displaying floating text

public class KeyScript : MonoBehaviour
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

    // Invokes method as to give time for key boolean to be changed (sometimes key would be destroyed before player could pick it up)
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SoundManagerScript.PlaySound("itemSound");
            Instantiate(floatingText, transform.position + transform.up * 0.6f, Quaternion.identity);
            Invoke("DisableKey", 0.1f);
        }
    }

    void DisableKey()
    {
        gameObject.SetActive(false);
        this.enabled = false;
    }
}
