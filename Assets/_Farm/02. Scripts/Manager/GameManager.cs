using Farm;
using UnityEngine;

public class GameManager : SingletonCore<GameManager>
{
    [SerializeField] private GameObject[] characterPrefabs;
    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        int index = DataManager.Instance.SelectCharacterIndex;
        GameObject character = Instantiate(characterPrefabs[index], spawnPoint.position, Quaternion.identity);
        DataManager.Instance.Player = character;
    }

    private void Start()
    {
        // 캐릭터 생성 이후에 카메라 속성 설정
        CameraManager.onSetProperty?.Invoke(DataManager.Instance.Player.transform);
    }
}
