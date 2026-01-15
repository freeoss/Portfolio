using UnityEngine;
using UnityEngine.TextCore.Text;

public class StateController : MonoBehaviour
{
    private IState currentState;

    private IdleState idle;
    private PatrolState patrol;
    private TraceState trace;
    private AttackState attack;

    private CharacterController cc;
    private Animator anim;
    private GameObject prefab;
    
    private void Start()
    {
        idle = new IdleState();
        patrol = new PatrolState();
        trace = new TraceState(this, cc, anim, prefab);
        attack = new AttackState();

        currentState = idle;
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.StateUpdate();
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {   
            SetState(idle);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            SetState(patrol);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SetState(trace);    
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SetState(attack);
        }
    }

    public void SetState(IState newState)
    {
        if (currentState != newState)
        {
            currentState?.StateExit();  // 기존 상태의 Exit
            
            currentState = newState;    // 상태 변경
            
            currentState?.StateEnter(); // 새로운 상태의 Enter
        }
    }
}
