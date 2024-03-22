using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawns Overworld enemy, so long as enemy hasn't been killed before

public class OverworldSpawner : MonoBehaviour
{
    public static bool hasDied = false;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        if (hasDied == false)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            hasDied = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
