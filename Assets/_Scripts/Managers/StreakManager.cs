using System;
using UnityEngine;

public class StreakManager : MonoBehaviour
{
    public static StreakManager Instance { get; private set; }
    private int multiplier = 1;

    private int scoreStreak = 0;
    private bool isStreakActive = false;

    public event EventHandler<OnStreakChangedEventArgs> OnStreakChanged;
    public class OnStreakChangedEventArgs : EventArgs
    {
        public bool isStreakActive;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void InscreaseScoreStreak()
    {
        scoreStreak++;
        if (scoreStreak == 3)
        {
            ActivateStreak();
        }
    }

    public bool IsStreakActive()
    {
        return isStreakActive;
    }

    private void ActivateStreak()
    {
        isStreakActive = true;
        multiplier = 2;
        OnStreakChanged?.Invoke(this, new OnStreakChangedEventArgs
        {
            isStreakActive = isStreakActive
        });
    }
    public void DeactivateStreak()
    {
        isStreakActive = false;
        scoreStreak = 0;
        multiplier = 1;
        OnStreakChanged?.Invoke(this, new OnStreakChangedEventArgs
        {
            isStreakActive = isStreakActive
        });
    }

    public int GetMultipler()
    {
        return multiplier;
    }
}
