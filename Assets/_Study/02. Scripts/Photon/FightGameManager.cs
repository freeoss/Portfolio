using Photon.Pun;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class FightGameManager : MonoBehaviourPun
{
    public GameObject cameraPrefab;
    
    void Awake()
    {
        int randomIndex = Random.Range(0, 4);
        string characterName = $"Player_{randomIndex}";
        
        GameObject character = PhotonNetwork.Instantiate(characterName, Vector3.up, Quaternion.identity);
        GameObject camera = Instantiate(cameraPrefab);

        camera.GetComponent<CinemachineCamera>().Follow = character.transform.GetChild(0);
    }
}