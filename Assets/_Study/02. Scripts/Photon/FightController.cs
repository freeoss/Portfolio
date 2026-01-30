using System.Collections;
using StarterAssets;
using UnityEngine;

public class FightController : MonoBehaviour
{
    public ThirdPersonController controller;
    private Animator anim;

    [SerializeField] private GameObject punchBox;
    [SerializeField] private GameObject kickBox;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<ThirdPersonController>();
    }

    void OnPunch()
    {
        StartCoroutine(PunchRoutine());
    }

    IEnumerator PunchRoutine()
    {
        anim.SetTrigger("Punch");
        FreezeSpeed(true);
        
        yield return new WaitForSeconds(0.5f);
        punchBox.SetActive(true);

        yield return new WaitForSeconds(0.3f);
        punchBox.SetActive(false);
        FreezeSpeed(false);
    }

    void OnKick()
    {
        StartCoroutine(KickRoutine());
    }

    IEnumerator KickRoutine()
    {
        anim.SetTrigger("Kick");
        FreezeSpeed(true);

        yield return new WaitForSeconds(0.6f);
        kickBox.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        kickBox.SetActive(false);
        FreezeSpeed(false);
    }

    public void FreezeSpeed(bool isStop)
    {
        // controller.MoveSpeed = isStop ? 0f : 2f;
        // controller.SprintSpeed = isStop ? 0f : 5.335f;
    }

    public void SetDeath()
    {
        controller.enabled = false;
        this.enabled = false;
    }
}