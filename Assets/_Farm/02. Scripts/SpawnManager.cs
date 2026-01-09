using Farm;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] characterPrefabs;
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        int index = DataManager.Instance.SelectCharacterIndex;

        Instantiate(characterPrefabs[index], spawnPoint.position, Quaternion.identity);
    }
}
