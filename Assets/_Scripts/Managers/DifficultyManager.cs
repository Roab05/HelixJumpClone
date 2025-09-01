using UnityEngine;
public class DifficultyManager
{
    public static int GetDifficulty()
    {
        int level = LevelManager.Instance.GetLevel();

        // Weights decrease for easy, increase for medium/hard as level increases
        float easyWeight = Mathf.Max(20f, 80f - level * 1.15f);    // Easy starts high, decreases
        float mediumWeight = Mathf.Min(50f, 10f + level * 1.15f);    // Medium increases
        float hardWeight = Mathf.Min(30f, 10f + level * 0.65f);           // Hard increases

        float totalWeight = easyWeight + mediumWeight + hardWeight;
        float rand = Random.value * totalWeight;

        if (rand < easyWeight)        
        {
            return 0; // Easy
        }
        else if (rand < easyWeight + mediumWeight)
        {
            return 1; // Medium
        }
        else
        {
            return 2; // Hard
        }
    }
}
