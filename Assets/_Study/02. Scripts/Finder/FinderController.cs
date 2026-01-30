using System.Collections;
using StarterAssets;
using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinderController : NetworkBehaviour
{
    private ThirdPersonController controller;
    private Animator anim;

    public GameObject punchHitbox;
    public GameObject kickHitbox;
    
    private bool isDead = false;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        
        anim = GetComponent<Animator>();
        controller = GetComponent<ThirdPersonController>();

        if (IsOwner)
        {
            var camera = FindFirstObjectByType<CinemachineCamera>();
            camera.Follow = transform.GetChild(0).transform;    // PlayerCameraRoot
            
            // FinderGameManager.Instance.NpcSpawnServerRpc();
            StartCoroutine(DelayRoutine());
        }
        else
        {
            GetComponent<PlayerInput>().enabled = false;
        }
    }

    IEnumerator DelayRoutine()
    {
        yield return new WaitUntil(() => FinderGameManager.Instance != null && FinderGameManager.Instance.IsSpawned);

        var spawnPoints = FinderGameManager.Instance.spawnPoints;
        int randomIndex = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[randomIndex].transform.position;
        
        FinderGameManager.Instance.NpcSpawnServerRpc();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead)
        {
            return;
        }
        
        var hitbox = other.GetComponent<Hitbox>();
        if (hitbox != null)
        {
            NetworkObject otherUser = other.transform.root.GetComponent<NetworkObject>();
            string otherID = otherUser.OwnerClientId.ToString();
            string myID = OwnerClientId.ToString();
            LogManager.Instance.SetLogServerRpc(otherID, myID, true);
            GetHit();
        }
    }

    void OnPunch(InputValue value)
    {
        if (IsOwner)
        {
            PunchServerRpc();
        }
    }
    
    [ServerRpc]
    void PunchServerRpc()
    {
        StartCoroutine(PunchRoutine());
    }

    IEnumerator PunchRoutine()
    {
        anim.SetTrigger("Punch");
        yield return new WaitForSeconds(0.5f);
        punchHitbox.SetActive(true);
        
        yield return new WaitForSeconds(0.3f);
        punchHitbox.SetActive(false);
    }

    void OnKick(InputValue value)
    {
        if (IsOwner)
        {
            KickServerRpc();
        }
    }

    [ServerRpc]
    void KickServerRpc()
    {
        StartCoroutine(KickRoutine());
    }

    IEnumerator KickRoutine()
    {
        anim.SetTrigger("Kick");
        
        yield return new WaitForSeconds(0.6f);
        kickHitbox.SetActive(true);
        
        yield return new WaitForSeconds(0.2f);
        kickHitbox.SetActive(false);
    }

    void GetHit()
    {
        isDead = true;
        anim.SetTrigger("Death");
        controller.enabled = false;
    }
}
