using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class MineralEvent : NetworkBehaviour
{
    public DigScoreManager scoreManager;
    private bool isDrop = false;

    IEnumerator Start()
    {
        scoreManager = FindFirstObjectByType<DigScoreManager>();
        
        isDrop = false;
        yield return new WaitForSeconds(1f);

        isDrop = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isDrop)
        {
            Debug.Log("광물 획득");
            GetMineralServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void GetMineralServerRpc()
    {
        scoreManager.AddScore();
        // gameObject.SetActive(false);
        GetComponent<NetworkObject>().Despawn(true);
    }
}
