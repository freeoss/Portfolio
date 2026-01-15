using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;

    private void OnEnable()
    {
        EventBus.scoreChanged += PrintScore;
    }

    private void OnDisable()
    {
        EventBus.scoreChanged -= PrintScore;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            score++;
            EventBus.onScoreChanged(score);
        }
    }

    private void PrintScore(int score)
    {
        Debug.Log($"현재 점수 : {score}");
    }
}
