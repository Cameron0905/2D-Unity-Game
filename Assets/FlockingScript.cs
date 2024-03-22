using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for boid attraction to slime

public class FlockingScript : MonoBehaviour
{
    public Transform Slime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame, checks if boid is not close enough to target and moves boid to said target if it is too far away
    void Update()
    {
        if (Vector3.Distance(Slime.position, transform.position) >= 0.4f)
        {
            Vector3 direction = Slime.position - transform.position;
            transform.Translate(direction * Time.deltaTime * 0.7f);
        }
        
    }

    
}
