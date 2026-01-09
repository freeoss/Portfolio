using System;
using UnityEngine;

public class StudyFunc : MonoBehaviour
{
    public Func<int, int, int> myFunc;

    private void Start()
    {
        myFunc = AddMethod;
        int result = myFunc(5, 2);
        Debug.Log(result);
    }

    public int AddMethod(int a, int b)
    {
        return a + b;
    }
}
