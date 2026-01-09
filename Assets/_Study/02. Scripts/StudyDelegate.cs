using System.Collections;
using UnityEngine;

public class StudyDelegate : MonoBehaviour
{
    public UIClass uiClass;
    public ParticleClass particleClass;
    public SoundClass soundClass;
    
    public delegate void TimerStart();
    public static event TimerStart onTimerStart;

    public delegate void TimerEnd();
    public static TimerEnd onTimerEnd;
    
    public delegate void TimerStop();
    public static TimerStop onTimerStop;

    public KeyCode keyCode = KeyCode.Space;
    
    public float timer = 5f;
    public bool isTimer = true;

    public GameObject timerStartUI;
    public ParticleSystem particle;
    
    private void Awake()
    {
        onTimerStart += StartEvent;
        onTimerStop += StopEvent;
        onTimerEnd += EndEvent;
    }

    private void Start()
    {
        onTimerStart?.Invoke();
        
        StartCoroutine(TimerRoutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            onTimerStop?.Invoke();
        }
    }

    IEnumerator TimerRoutine()
    {
        while (isTimer)
        {
            yield return new WaitForSeconds(1f);
            timer--;

            if (timer <= 0f)
            {
                isTimer = false;
                onTimerEnd?.Invoke();
            }
        }
    }

    private void StartEvent()
    {
        Debug.Log("폭탄: 설치");
    }

    private void StopEvent()
    {
        isTimer = false;
        StopAllCoroutines();
        
        Debug.Log("폭탄: 해체");
    }

    private void EndEvent()
    {
        Debug.Log("폭탄: 폭발");
    }
}
