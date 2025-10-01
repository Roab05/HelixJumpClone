using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const string HIGH_SCORE_KEY = "HighScore";

    public static ScoreManager Instance { get; private set; }
    public bool isNewBest = false;

    private int score;
    private int bestScore; 

    public event EventHandler OnScoreChanged;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        score = 0;
        bestScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
    }

    private void Start()
    {
        Ball.Instance.OnRingPlatformPassed += Ball_OnRingPlatformPassed;
        Ball.Instance.OnDead += Ball_OnDead;
    }

    private void Ball_OnDead(object sender, EventArgs e)
    {
        if (score > bestScore)
        {
            bestScore = score;
            isNewBest = true;
        }
        PlayerPrefs.SetInt(HIGH_SCORE_KEY, bestScore);
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
    public string GetBestScore()
    {
        if (isNewBest)
            return "NEW BEST SCORE!";
        return $"BEST SCORE\n{bestScore}";
    }

    private void OnDestroy()
    {
        Ball.Instance.OnRingPlatformPassed -= Ball_OnRingPlatformPassed;
        Ball.Instance.OnDead -= Ball_OnDead;
    }
}
