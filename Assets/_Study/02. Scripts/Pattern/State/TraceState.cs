using UnityEngine;

public class TraceState : IState
{
    private CharacterController cc;
    private Transform target;
    private Animator anim;
    private GameObject prefab;
    private MonoBehaviour mono;
    
    public TraceState(MonoBehaviour mono, CharacterController cc, Animator anim, GameObject prefab)
    {
        this.cc = cc;
        this.anim = anim;
        this.prefab = prefab;
        this.mono = mono;
    }
    
    public void StateEnter()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // GameObject obj = mono.Instantiate(prefab);
        
        Debug.Log("추격 시작");
    }

    public void StateUpdate()
    {
        Vector3 moveDir = (target.position - mono.transform.position).normalized;
        cc.Move(moveDir * 5f * Time.deltaTime);
        
        Debug.Log("추격중");
    }

    public void StateExit()
    {
        Debug.Log("추격종료");
    }
}
