using TMPro;
using Unity.Netcode;
using UnityEngine;

public class DigScoreManager : NetworkBehaviour
{
    public TextMeshProUGUI scoreText;
    // private int score;
    private NetworkVariable<int> networkScore = new NetworkVariable<int>();

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        
        scoreText.text = $"현재 획득한 광물의 수 : {networkScore.Value}";

        networkScore.OnValueChanged += SetScore;
    }

    public void AddScore()
    {
        networkScore.Value++;
    }

    private void SetScore(int preValue, int newValue)
    {
        scoreText.text = $"현재 획득한 광물의 수 : {newValue}";
    }
}
