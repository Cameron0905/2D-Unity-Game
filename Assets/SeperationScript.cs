using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for boid seperation between other boids

public class SeperationScript : MonoBehaviour
{

    GameObject[] Flock;

    // Start is called before the first frame update
    void Start()
    {
        Flock = GameObject.FindGameObjectsWithTag("Flock");
    }

    // Update is called once per frame, checks seperation distance between boids, ensuring that boids prefer staying away from one another
    void Update()
    {
        foreach(GameObject boid in Flock)
        {
            if (boid != gameObject)
            {
                float boidDistance = Vector3.Distance(boid.transform.position, this.transform.position);
                if (boidDistance <= 0.3f)
                {
                    Vector3 direction = transform.position - boid.transform.position;
                    transform.Translate(direction * Time.deltaTime);
                }
            }
        }
    }
}
