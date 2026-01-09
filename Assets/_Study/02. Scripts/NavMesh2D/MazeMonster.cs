using UnityEngine;
using UnityEngine.AI;

public class MazeMonster : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        agent.SetDestination(player.position);
    }
}
