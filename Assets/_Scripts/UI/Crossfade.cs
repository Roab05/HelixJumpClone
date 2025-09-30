using UnityEngine;

public class Crossfade : MonoBehaviour
{
    private const string FADE_START = "FadeStart";

    public static Crossfade Instance { get; private set; }

    private Animator animator;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCrossfade())
        {
            animator.Play(FADE_START);
        }
    }
}
