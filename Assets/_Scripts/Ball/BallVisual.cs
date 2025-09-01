using UnityEngine;

public class BallVisual : MonoBehaviour
{
    private const string BALL_BOUNCE_TRIGGER = "BallBounce";

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
        animator.SetTrigger(BALL_BOUNCE_TRIGGER);
    }

    private void OnDestroy()
    {
        Ball.Instance.OnCollided -= Ball_OnCollided;
    }
}
