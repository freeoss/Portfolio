using UnityEngine;

public class SoundClass : MonoBehaviour
{
    void Awake()
    {
        TimerDelegate.onTimerStart += StartSound;
        TimerDelegate.onTimerStop += StopSound;
        TimerDelegate.onTimerEnd += BombSound;
    }

    public void StartSound()
    {
        Debug.Log("시작 사운드");
    }
    
    public void StopSound()
    {
        Debug.Log("해제 사운드");
    }
    
    public void BombSound()
    {
        Debug.Log("터지는 사운드");
    }
}