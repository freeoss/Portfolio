using UnityEngine;

public class AttackState : IState
{
    public void StateEnter()
    {
        Debug.Log("공격 시작");
    }

    public void StateUpdate()
    {
        Debug.Log("공격중");
    }

    public void StateExit()
    {
        Debug.Log("공격 종료");
    }
}
