using UnityEngine;

public class UIClass : MonoBehaviour
{
    void Awake()
    {
        TimerDelegate.onTimerStart += StartUI;
        TimerDelegate.onTimerStop += StopUI;
        TimerDelegate.onTimerEnd += EndUI;
    }
    
    public void StartUI()
    {
        Debug.Log("타이머 UI On");
    }
    
    public void StopUI()
    {
        Debug.Log("폭탄 해제 UI On");
    }
    
    public void EndUI()
    {
        Debug.Log("실패 UI On");
    }
}