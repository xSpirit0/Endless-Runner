using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    private float startZ;
    private bool gameOver = false;

    private int coinScore = 0;
    private int bestScore;

    void Start()
    {
        startZ = player.position.z;

        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "Best: " + bestScore;
    }

    void Update()
    {
        if (gameOver)
            return;

        float distance = player.position.z - startZ;
        int distanceScore = Mathf.Max(0, Mathf.FloorToInt(distance));

        int totalScore = distanceScore + coinScore;

        scoreText.text = "Score: " + totalScore;

        // Update best score LIVE
        if (totalScore > bestScore)
        {
            bestScore = totalScore;
            bestScoreText.text = "Best: " + bestScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
    }

    public void AddScore(int amount)
    {
        coinScore += amount;
    }

    public void StopScore()
    {
        gameOver = true;

        float distance = player.position.z - startZ;
        int distanceScore = Mathf.Max(0, Mathf.FloorToInt(distance));

        int totalScore = distanceScore + coinScore;

        scoreText.text = "Score: " + totalScore;
    }
}