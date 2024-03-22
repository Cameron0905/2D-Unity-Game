using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Uses NavMesh2D package to draw 2d NavMesh for ghosts to use as pathfinding, as well as some ghost behaviours

public class NavMeshScript : MonoBehaviour
{
    [SerializeField]
    Transform target;
    NavMeshAgent agent;
    private float timeSinceGrowl;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        timeSinceGrowl += Time.deltaTime;
        if (timeSinceGrowl > 2.5f)
        {
            timeSinceGrowl = 0;
            SoundManagerScript.PlaySound("ghostSound");
        }
    }
}
