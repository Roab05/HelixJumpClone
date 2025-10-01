using UnityEngine;

public class BallVisual : MonoBehaviour
{
    private const string BALL_BOUNCE = "BallBounceAnimationClip";

    [SerializeField] private Material ballMaterial;
    [SerializeField] private Material ballStreakMaterial;

    private MeshRenderer meshRenderer;
    private Animator animator;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Ball.Instance.OnCollided += Ball_OnCollided;
        StreakManager.Instance.OnStreakChanged += StreakManager_OnStreakChanged;
    }

    private void StreakManager_OnStreakChanged(object sender, StreakManager.OnStreakChangedEventArgs e)
    {
        if (e.isStreakActive)
        {
            meshRenderer.material = ballStreakMaterial;
        }
        else
        {
            meshRenderer.material = ballMaterial;
        }
        BallTrail.Instance.SetColor(meshRenderer.material);
    }

    private void Ball_OnCollided(object sender, System.EventArgs e)
    {
        animator.Play(BALL_BOUNCE, 0, 0f);
    }
    private void OnDestroy()
    {
        StreakManager.Instance.OnStreakChanged -= StreakManager_OnStreakChanged;
        Ball.Instance.OnCollided -= Ball_OnCollided;
    }
}
