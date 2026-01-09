using UnityEngine;
using UnityEngine.AI;

public class MazeMonster : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        agent.SetDestination(player.position);
    }
}