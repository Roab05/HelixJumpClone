using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class BallTrail : MonoBehaviour
{
    public static BallTrail Instance { get; private set; }
    private TrailRenderer trailRenderer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        Ball.Instance.OnGoalReached += Ball_OnGoalReached;
    }

    private void Ball_OnGoalReached(object sender, System.EventArgs e)
    {
        StartCoroutine(TrailChange());
    }

    IEnumerator TrailChange()
    {
        ClearTrail();

        yield return null; // Wait 1 frame

        ClearTrail();
    }
    private void ClearTrail()
    {
        trailRenderer.Clear();
    }
    public void SetColor(Material material)
    {
        trailRenderer.startColor = material.color;
        trailRenderer.endColor = material.color;
    }
    private void OnDisable()
    {
        Ball.Instance.OnGoalReached -= Ball_OnGoalReached;
    }

}
