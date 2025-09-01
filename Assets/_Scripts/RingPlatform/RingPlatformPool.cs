using NUnit.Framework;
using System;
using UnityEngine;

public class RingPlatformPool : MonoBehaviour
{
    public static RingPlatformPool Instance { get; private set; }

    [SerializeField] private RingPlatformVisualPrefsSO ringPlatformVisualPrefsSO;
    [SerializeField] private Transform ringPlatformStartTransform;
    [SerializeField] private Transform ringPlatformGoalTransform;
    [SerializeField] private Transform ringPlatformPrefab;

    private Transform[] ringPlatformVisualPrefs;

    private float spawnPosition;
    private int spawnAmount;

    public event EventHandler OnGoalReached;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        spawnPosition = ringPlatformStartTransform.position.y - 1f;
        spawnAmount = Mathf.RoundToInt(Mathf.Abs(ringPlatformStartTransform.position.y - ringPlatformGoalTransform.position.y - 1f));

        SpawnRingPlatforms();
    }

    private void Start()
    {
        Ball.Instance.OnCollided += Ball_OnCollided;
    }

    private void Ball_OnCollided(object sender, Ball.OnCollidedEventArgs e)
    {
        if (e.ringPlatformTransform == ringPlatformGoalTransform)
        {
            OnGoalReached?.Invoke(this, EventArgs.Empty);
            ResetPool();
        }
    }

    private void ResetPool()
    {
        foreach (Transform ringPlatformTransform in transform)
        {
            if (ringPlatformTransform == ringPlatformGoalTransform 
             || ringPlatformTransform == ringPlatformStartTransform)
                continue;

            ringPlatformTransform.gameObject.SetActive(true);
            RingPlatform ringPlatform = ringPlatformTransform.GetComponent<RingPlatform>();

            var newVisualTransform = Instantiate(GetRingPlatformVisual(), ringPlatformTransform);
            var newVisual = newVisualTransform.GetComponent<RingPlatformVisual>();

            ringPlatform.SetNewVisual(newVisual);
        }
    }

    private void SpawnRingPlatforms()
    {
        for (int spawnIndex = 0; spawnIndex < spawnAmount; ++spawnIndex)
        {
            Transform ringPlatformTransform = Instantiate(ringPlatformPrefab, transform);
            Instantiate(GetRingPlatformVisual(), ringPlatformTransform);
            ringPlatformTransform.position = new Vector3(0f, spawnPosition, 0f);
            ringPlatformTransform.localEulerAngles = new Vector3(0f, UnityEngine.Random.Range(0f, 360f), 0f);
            spawnPosition -= 1;
        }
    }

    private Transform GetRingPlatformVisual()
    {
        int difficulty = DifficultyManager.GetDifficulty();
        if (difficulty == 0)
            ringPlatformVisualPrefs = ringPlatformVisualPrefsSO.RingPlatformVisualPrefsEasy;
        else if (difficulty == 1)
            ringPlatformVisualPrefs = ringPlatformVisualPrefsSO.RingPlatformVisualPrefsMedium;
        else
            ringPlatformVisualPrefs = ringPlatformVisualPrefsSO.RingPlatformVisualPrefsHard;

        return ringPlatformVisualPrefs[UnityEngine.Random.Range(0, ringPlatformVisualPrefs.Length)];
    }

    public int GetSpawnAmount()
    {
        return spawnAmount;
    }

    private void OnDestroy()
    {
        Ball.Instance.OnCollided -= Ball_OnCollided;
    }
}
