using UnityEngine;

public class SoundClass : MonoBehaviour
{
    private void Awake()
    {
        StudyDelegate.onTimerStart += StartSound;
        StudyDelegate.onTimerStop += StopSound;
        StudyDelegate.onTimerEnd += EndSound;
    }

    public void StartSound()
    {
        Debug.Log("사운드: start");
    }

    public void StopSound()
    {
        Debug.Log("사운드: stop");
    }

    public void EndSound()
    {
        Debug.Log("사운드: End");
    }
}
