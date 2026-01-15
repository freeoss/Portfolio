using UnityEngine;

public class SingletonTest : SingletonCore<SingletonTest>
{
    protected override void Awake()
    {
        base.Awake();
        
        
    }

    public void LevelUp(int level)
    {
        level++;
    }
}
