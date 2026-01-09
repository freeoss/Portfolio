using UnityEngine;

public partial class StudyPartial : MonoBehaviour
{
    public int number1;
    public int number2;
    
    void Start()
    {
        MethodA();
    }

    partial void Method();
    
    public void MethodA()
    {
        
    }
}

public partial class StudyPartial : MonoBehaviour
{
    public int number3;

    partial void Method()
    {
        
    }
}