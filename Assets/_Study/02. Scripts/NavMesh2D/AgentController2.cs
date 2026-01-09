using UnityEngine;
using UnityEngine.AI;

public class AgentController2 : MonoBehaviour
{
    public Transform destination;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(destination.position);
    }
}
