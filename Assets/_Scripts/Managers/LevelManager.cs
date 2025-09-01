using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private int level = 1;
    
    public event EventHandler OnLevelChanged;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        RingPlatformPool.Instance.OnGoalReached += RingPlatformPool_OnGoalReached;
    }

    private void RingPlatformPool_OnGoalReached(object sender, System.EventArgs e)
    {
        level++;
        OnLevelChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetLevel()
    {
        return level;
    }

    private void OnDestroy()
    {
        RingPlatformPool.Instance.OnGoalReached -= RingPlatformPool_OnGoalReached;
    }
}
