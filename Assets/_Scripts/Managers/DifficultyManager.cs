using System.Collections.Generic;
using UnityEngine;
public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private float baseEasyRate = 0.8f;
    public List<int> GetPlatformDifficultyList()
    {
        int level = LevelManager.Instance.GetLevel();
        int platformCount = RingPlatformPool.Instance.GetPlatformAmount();
        float easyRate = Mathf.Max(0.2f, baseEasyRate - (level - 1) * 0.04f);

        int easyCount = Mathf.RoundToInt(platformCount * easyRate);
        int mediumCount = Mathf.RoundToInt(platformCount * ((1f - easyRate) * (2/3f)));
        int hardCount = platformCount - easyCount - mediumCount;

        List<int> difficultyList = new();
        for (int i = 0; i < easyCount; i++)
            difficultyList.Add(0);
        for (int i = 0; i < mediumCount; i++)
            difficultyList.Add(1);
        for (int i = 0; i < hardCount; i++)
            difficultyList.Add(2);

        // Shuffle the list
        for (int i = platformCount - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (difficultyList[i], difficultyList[j]) = (difficultyList[j], difficultyList[i]);
        }

        return difficultyList;
    }

    public float GetPlatformSpinSpeed()
    {
        var level = LevelManager.Instance.GetLevel();
        return Random.Range(20f, 21f + (level - 1) * 1.75f);
    }
}
