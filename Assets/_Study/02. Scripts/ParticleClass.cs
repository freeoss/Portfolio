using UnityEngine;

public class ParticleClass : MonoBehaviour
{
    public ParticleSystem ps;

    private void Awake()
    {
        StudyDelegate.onTimerEnd += Explosion;
    }

    public void Explosion()
    {
        Debug.Log("파티클: Play");
        // ps.Play();
    }
}
