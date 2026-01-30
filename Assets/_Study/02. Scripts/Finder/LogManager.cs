using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class LogManager : NetworkBehaviour
{
    public static LogManager Instance;

    public TextMeshProUGUI logText;
    
    private NetworkVariable<FixedString128Bytes> logMessage = new NetworkVariable<FixedString128Bytes>();

    private string preMsg;
    
    private void Awake()
    {
        Instance = this;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        logMessage.OnValueChanged += SetLog;
    }

    [ServerRpc(AllowTargetOverride = false)]
    public void SetLogServerRpc(string kill, string death, bool isUser)
    {
        string color = isUser ? "red" : "grey";
        string msg = $"<color={color}>\n{kill}님이 : {death} 님을 처치하였습니다.</color>";

        if (preMsg == msg)
        {
            msg += " ";
        }
        
        logMessage.Value = msg;
    }

    private void SetLog(FixedString128Bytes preValue, FixedString128Bytes newValue)
    {
        logText.text += newValue;
    }
}
