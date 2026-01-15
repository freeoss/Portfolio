using UnityEngine;

public class BasicFSM : MonoBehaviour
{
    public enum MonsterState { Idle, Partol, Trace, Attack }

    public MonsterState monsterState = MonsterState.Idle;

    private void Update()
    {
        switch (monsterState)
        {
            case MonsterState.Idle:
                Debug.Log("Idle: 몬스터: 대기");
                break;
            case MonsterState.Partol:
                Debug.Log("Idle: 몬스터: 정찰");
                break;
            case MonsterState.Trace:
                Debug.Log("Idle: 몬스터: 추격");
                break;
            case MonsterState.Attack:
                Debug.Log("Idle: 몬스터: 공격");
                break;
        }
    }

    public void SetState(MonsterState newState)
    {
        if (monsterState != newState)
        {
            monsterState = newState;
        }
    }
}
