using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ConnectManager : MonoBehaviour
{
    public Button hostButton;
    public Button serverButton;
    public Button clientButton;
    public Button closeButton;

    private void Start()
    {
        hostButton.onClick.AddListener(() => { NetworkManager.Singleton.StartHost(); });
        serverButton.onClick.AddListener(() => { NetworkManager.Singleton.StartServer(); });
        clientButton.onClick.AddListener(() => { NetworkManager.Singleton.StartClient(); });
        closeButton.onClick.AddListener(() => { NetworkManager.Singleton.Shutdown(); });

        NetworkManager.Singleton.OnServerStarted += ServerStated;
        NetworkManager.Singleton.OnServerStopped += ServerStoped;
        NetworkManager.Singleton.OnClientStarted += ClientStated;
    }

    private void ServerStated()
    {
        Debug.Log("서버 시작");
    }

    private void ServerStoped(bool isStoped)
    {
        Debug.Log("서버 종료");
    }

    private void ClientStated()
    {
        Debug.Log("클라이언트 접속");
    }
}
