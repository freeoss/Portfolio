using UnityEngine;
using UnityEngine.AI;

public class AgentController2 : MonoBehaviour
{
    private NavMeshAgent agent;

    public Transform destination;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(destination.position);
    }
}