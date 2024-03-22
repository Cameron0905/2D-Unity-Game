using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script which spawns Grunt enemies

public class EnemySpawner : MonoBehaviour
{

    // Initialises appropiate states

    private Transform player;
    public GameObject enemy;
    public GameObject goldenKey;
    public float innerRing;
    public int enemyCount = 0;
    float distanceToPlayer;
    float randX;
    float randY;
    
    // Start is called before the first frame update, Repeatedly runs enemy-spawning method
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("SpawnEnemies", 2f, 4f);
    }

    // Update is called once per frame, generates random coordinates around a certain point
    void Update()
    {
        distanceToPlayer = Vector2.Distance(player.position, transform.position);
        randX = Random.Range(transform.position.x - innerRing, transform.position.x + innerRing);
        randY = Random.Range(transform.position.y - innerRing, transform.position.y + innerRing);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, innerRing);
    }

    // Spawns enemies until there are five, then spawns key after delay
    void SpawnEnemies()
    {
        if (distanceToPlayer > innerRing && enemyCount < 3)
        {
            enemyCount += 1;
            Instantiate(enemy, new Vector2(randX, randY), Quaternion.identity);
        }
        if (enemyCount == 3)
        {
            Invoke("SpawnKey", 8f);
        }
    }

    // Spawns third key in front of door
    void SpawnKey()
    {
        Instantiate(goldenKey, new Vector2(4f, -3f), Quaternion.identity);
        CancelInvoke();
    }
}
