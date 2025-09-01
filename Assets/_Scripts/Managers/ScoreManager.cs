using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int score;

    public event EventHandler OnScoreChanged;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        score = 0;
    }

    private void Start()
    {
        Ball.Instance.OnRingPlatformPassed += Ball_OnRingPlatformPassed;
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

    private void OnDestroy()
    {
        Ball.Instance.OnRingPlatformPassed -= Ball_OnRingPlatformPassed;
    }
}
