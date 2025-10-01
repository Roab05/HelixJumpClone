using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private const string HIGH_LEVEL_KEY = "HighLevel";

    public static LevelManager Instance { get; private set; }

    private int level;
    private int highLevel;

    public event EventHandler OnLevelChanged;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        level = 1;
        highLevel = PlayerPrefs.GetInt(HIGH_LEVEL_KEY, 1);
    }

    private void Start()
    {
        Ball.Instance.OnGoalReached += Ball_OnGoalReached;
        Ball.Instance.OnDead += Ball_OnDead;
    }

    private void Ball_OnDead(object sender, EventArgs e)
    {
        highLevel = Mathf.Max(highLevel, level);
        level = 1;
        PlayerPrefs.SetInt(HIGH_LEVEL_KEY, highLevel);
        OnLevelChanged?.Invoke(this, EventArgs.Empty);
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
    public int GetHighLevel()
    {
        return highLevel;
    }

    private void OnDestroy()
    {
        Ball.Instance.OnGoalReached -= Ball_OnGoalReached;
        Ball.Instance.OnDead -= Ball_OnDead;
    }
}
