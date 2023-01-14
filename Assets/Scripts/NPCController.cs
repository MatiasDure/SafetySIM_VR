using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    [SerializeField] float walkRadius;
    [SerializeField] float newDestinationTimer;

    NavMeshAgent agent;

    Vector3 randomDirection;
    Vector3 finalDestination;

    bool findNewDest = true;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(findNewDest)
            StartCoroutine(RandomDestination());
    }

    IEnumerator RandomDestination()
    {
        randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
        finalDestination = hit.position;

        agent.SetDestination(finalDestination);

        findNewDest = false;
        yield return new WaitForSeconds(newDestinationTimer);
        findNewDest = true;
    }
}
