using System.Numerics;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Tilemaps;
using Quaternion = UnityEngine.Quaternion;

public class TilemapManager : NetworkBehaviour
{
    public Tilemap tilemap;
    public GameObject[] minerals;

    public NetworkList<Vector3Int> destroyedTiles = new NetworkList<Vector3Int>();

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        
        tilemap = GetComponent<Tilemap>();

        destroyedTiles.OnListChanged += OnTileDestroyed;

        foreach (var pos in destroyedTiles)
        {
            tilemap.SetTile(pos, null);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void RemoveTileServerRpc(Vector3Int cellPos)
    {
        RemoveTile(cellPos);
    }
    
    public void RemoveTile(Vector3Int cellPos)
    {
        int randomValue = Random.Range(0, 101);
        if (randomValue >= 70)  // 드롭률 30%
        {
            int randomIndex = Random.Range(0, minerals.Length);
            GameObject mineral = Instantiate(minerals[randomIndex], cellPos, Quaternion.identity);
            mineral.GetComponent<NetworkObject>().Spawn();
        }
        
        tilemap.SetTile(cellPos, null);
    }

    private void OnTileDestroyed(NetworkListEvent<Vector3Int> changedEvent)
    {
        if (changedEvent.Type == NetworkListEvent<Vector3Int>.EventType.Add)
        {
            tilemap.SetTile(changedEvent.Value, null);
        }
    }
}
