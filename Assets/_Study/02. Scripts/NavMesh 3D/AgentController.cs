using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    private NavMeshAgent agent;
    public NavMeshSurface surface;

    private float bakeDistance = 10f;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        surface. transform.position = transform.position;
        surface.BuildNavMesh();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        float dist = Vector3.Distance(transform.position, surface.transform.position);
        if (dist > bakeDistance)
        {
            surface. transform.position = transform.position;
            surface.BuildNavMesh();
        }
    }
}