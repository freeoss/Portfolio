using UnityEngine;

public abstract class MonsterCore : MonoBehaviour
{
    public abstract string Name { get; }
    public abstract void Attack();
}
