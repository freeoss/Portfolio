using UnityEngine;

public class UIClass : MonoBehaviour
{
    // public GameObject startUI;
    // public GameObject stopUI;
    // public GameObject endUI;

    private void Awake()
    {
        StudyDelegate.onTimerStart += StartUI;
        StudyDelegate.onTimerStop += StopUI;
        StudyDelegate.onTimerEnd += EndUI;
    }

    public void StartUI()
    {
        Debug.Log("UI: Start");
    }

    public void StopUI()
    {
        Debug.Log("UI: Stop");
    }

    public void EndUI()
    {
        Debug.Log("UI: End");
    }
}
