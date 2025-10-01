using System;
using System.Collections.Generic;
using UnityEngine;

public class RingPlatformPool : MonoBehaviour
{
    public static RingPlatformPool Instance { get; private set; }

    [SerializeField] private RingPlatformVisualPrefsSO ringPlatformVisualPrefsSO;
    [SerializeField] private Transform ringPlatformStartTransform;
    [SerializeField] private Transform ringPlatformGoalTransform;
    [SerializeField] private Transform ringPlatformPrefab;

    private Transform[] ringPlatformVisualPrefs;

    private int totalSpawnAmount = 50;
    private int ringPlatformStartTransformPositionY1 = 31;
    private int ringPlatformStartTransformPositionY2 = 42;

    public event EventHandler OnPoolReset;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        Ball.Instance.OnGoalReached += Ball_OnGoalReached;

        SpawnRingPlatforms();
        ResetPool();
    }

    private void Ball_OnGoalReached(object sender, EventArgs e)
    {
        ResetPool();
    }

    public void ResetPool()
    {
        ringPlatformStartTransform.position = new Vector3(0f, UnityEngine.Random.Range(ringPlatformStartTransformPositionY1, ringPlatformStartTransformPositionY2), 0f);

        List<int> difficultyList = DifficultyManager.Instance.GetPlatformDifficultyList();
        int difficultyIndex = 0;

        foreach (Transform ringPlatformTransform in transform)
        {
            if (ringPlatformTransform == ringPlatformGoalTransform
             || ringPlatformTransform == ringPlatformStartTransform)
                continue;

            if ((int)ringPlatformTransform.position.y < (int)ringPlatformStartTransform.position.y)
            {
                ringPlatformTransform.gameObject.SetActive(true);
                RingPlatform ringPlatform = ringPlatformTransform.GetComponent<RingPlatform>();

                var newVisualTransform = Instantiate(GetRingPlatformVisual(difficultyList[difficultyIndex]), ringPlatformTransform);
                var newVisual = newVisualTransform.GetComponent<RingPlatformVisual>();

                ringPlatform.SetNewVisual(newVisual);

                ++difficultyIndex;
            }
            else
            {
                ringPlatformTransform.gameObject.SetActive(false);
            }
        }

        OnPoolReset?.Invoke(this, EventArgs.Empty);
    }

    private void SpawnRingPlatforms()
    {
        var spawnPosition = (int)ringPlatformGoalTransform.position.y + 1;
        for (int spawnIndex = 0; spawnIndex < totalSpawnAmount; ++spawnIndex)
        {
            Transform ringPlatformTransform = Instantiate(ringPlatformPrefab, transform);
            ringPlatformTransform.position = new Vector3(0f, spawnPosition, 0f);
            ringPlatformTransform.localEulerAngles = new Vector3(0f, UnityEngine.Random.Range(0f, 360f), 0f);
            ringPlatformTransform.gameObject.SetActive(false);
            spawnPosition += 1;
        }
    }

    private Transform GetRingPlatformVisual(int difficulty)
    {
        if (difficulty == 0)
            ringPlatformVisualPrefs = ringPlatformVisualPrefsSO.RingPlatformVisualPrefsEasy;
        else if (difficulty == 1)
            ringPlatformVisualPrefs = ringPlatformVisualPrefsSO.RingPlatformVisualPrefsMedium;
        else
            ringPlatformVisualPrefs = ringPlatformVisualPrefsSO.RingPlatformVisualPrefsHard;

        return ringPlatformVisualPrefs[UnityEngine.Random.Range(0, ringPlatformVisualPrefs.Length)];
    }

    public int GetPlatformAmount()
    {
        return (int)ringPlatformStartTransform.position.y - (int)ringPlatformGoalTransform.position.y - 1;
    }

    public Transform GetRingPlatformStartTransform()
    {
        return ringPlatformStartTransform;
    }
    public Transform GetRingPlatformGoalTransform()
    {
        return ringPlatformGoalTransform;
    }
    private void OnDestroy()
    {
        Ball.Instance.OnGoalReached -= Ball_OnGoalReached;
    }
}
