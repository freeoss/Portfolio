using UnityEngine;
using UnityEngine.Events;

public class StudyUnityEvent : MonoBehaviour
{
    public UnityEvent uEvent;

    private void Start()
    {
        uEvent.AddListener(MethodA);
        uEvent.RemoveListener(MethodA);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            uEvent?.Invoke();
        }
    }

    void MethodA()
    {
        
    }
}
