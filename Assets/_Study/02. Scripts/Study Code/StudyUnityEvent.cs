using UnityEngine;
using UnityEngine.Events;

public class StudyUnityEvent : MonoBehaviour
{
    public delegate void MyDelegate();
    
    private UnityEvent uEvent;

    void Start()
    {
        // uEvent = MethodA;
        // uEvent += MethodA;
        // uEvent -= MethodA;

        uEvent.AddListener(MethodA);
        uEvent.RemoveListener(MethodA);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            uEvent?.Invoke();
        }
    }

    public void AddMethod(UnityAction action)
    {
        uEvent.AddListener(action);
    }

    public void MethodA()
    {
        
    }
}