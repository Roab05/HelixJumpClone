using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const string HIGH_SCORE_KEY = "HighScore";

    public static ScoreManager Instance { get; private set; }
    private int score;
    private int highScore; 

    public event EventHandler OnScoreChanged;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        score = 0;
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
    }

    private void Start()
    {
        Ball.Instance.OnRingPlatformPassed += Ball_OnRingPlatformPassed;
        Ball.Instance.OnDead += Ball_OnDead;
    }

    private void Ball_OnDead(object sender, EventArgs e)
    {
        highScore = Math.Max(highScore, score);
        score = 0;
        PlayerPrefs.SetInt(HIGH_SCORE_KEY, highScore);
        OnScoreChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Ball_OnRingPlatformPassed(object sender, EventArgs e)
    {
        score += LevelManager.Instance.GetLevel() * StreakManager.Instance.GetMultipler();
        OnScoreChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetScore()
    {
        return score;
    }
    public int GetHighScore()
    {
        return highScore;
    }

    private void OnDestroy()
    {
        Ball.Instance.OnRingPlatformPassed -= Ball_OnRingPlatformPassed;
        Ball.Instance.OnDead -= Ball_OnDead;
    }
}
