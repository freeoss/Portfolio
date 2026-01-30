using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpController : MonoBehaviourPun
{
    private Animator anim;
    private IHitbox hitbox;

    public TextMeshPro nicknametext;
    public Image hpBar;

    public float currentHp = 100f;
    public float maxHp = 100f;

    private void Start()
    {
        anim = GetComponent<Animator>();

        if (photonView.IsMine)
        {
            nicknametext.text = PhotonNetwork.NickName;
            nicknametext.color = Color.green;
        }
        else
        {
            nicknametext.text = photonView.Owner.NickName;
            nicknametext.color = Color.red;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentHp <= 0)    // Death 상태에서 동작을 막기 위해
        {
            return;
        }
        
        hitbox = other.GetComponent<IHitbox>();
        if (hitbox != null)
        {
            OnGetDamage(hitbox.Damage);
        }
    }

    private void OnGetDamage(float damage)
    {
        currentHp -= damage;

        hpBar.fillAmount = currentHp / maxHp;
        
        if (currentHp <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        GetComponent<FightController>().SetDeath();
        anim.SetTrigger("Death");
        
        if (photonView.IsMine)
        {
            Fade.onFadeAction?.Invoke(3f, Color.black, true);
        }
    }
}
