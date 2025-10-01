using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private const string HIGH_LEVEL_KEY = "HighLevel";

    public static LevelManager Instance { get; private set; }
    public bool isNewBest = false;

    private int level;
    private int bestLevel;

    public event EventHandler OnLevelChanged;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        level = 1;
        bestLevel = PlayerPrefs.GetInt(HIGH_LEVEL_KEY, 0);
    }

    private void Start()
    {
        Ball.Instance.OnGoalReached += Ball_OnGoalReached;
        Ball.Instance.OnDead += Ball_OnDead;
    }

    private void Ball_OnDead(object sender, EventArgs e)
    {
        if (level > bestLevel)
        {
            bestLevel = level;
            isNewBest = true;
        }
        PlayerPrefs.SetInt(HIGH_LEVEL_KEY, bestLevel);
    }

    private void Ball_OnGoalReached(object sender, EventArgs e)
    {
        level++;
        OnLevelChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetLevel()
    {
        return level;
    }
    public string GetBestLevel()
    {
        if (isNewBest)
            return "NEW BEST LEVEL!";
        return $"BEST LEVEL\n{bestLevel}";
    }

    private void OnDestroy()
    {
        Ball.Instance.OnGoalReached -= Ball_OnGoalReached;
        Ball.Instance.OnDead -= Ball_OnDead;
    }
}
