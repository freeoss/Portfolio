using UnityEngine;

public class StudySealed : MonoBehaviour
{
    
}

public class ParentClass : MonoBehaviour
{
    public virtual void Method()
    {
        Debug.Log("Parent Class");
    }
}

public class ChildClass : ParentClass
{
    void Start()
    {
        Method();
    }

    public sealed override void Method()
    {
        Debug.Log("Child Class");
    }
}

public class BabyClass : ChildClass
{
    void Start()
    {
        Method();
    }

    // 작동 X
    // public override void Method()
    // {
    //     Debug.Log("Child Class");
    // }
}