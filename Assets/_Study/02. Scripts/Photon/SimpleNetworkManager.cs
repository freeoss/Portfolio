using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleNetworkManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField nicknameInput;
    public Button connectButton;
    
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;

        PhotonNetwork.GameVersion = "1";
    }

    private void Start()
    {
        connectButton.onClick.AddListener(Connect);
    }

    private void Connect()
    {
        PhotonNetwork.NickName = nicknameInput.text;
        
        PhotonNetwork.ConnectUsingSettings();
        
        Debug.Log("서버 접속");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 20 }, null);
        Debug.Log("서버 접속 완료");
    }
    
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
        // PhotonNetwork.Instantiate("Player", Vector3.up, Quaternion.identity);
        // Debug.Log("캐릭터 생성");
    }
}
