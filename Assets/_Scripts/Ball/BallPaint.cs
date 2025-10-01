using UnityEngine;
using UnityEngine.UI;

public class BallPaint : MonoBehaviour
{
    public static BallPaint Instance { get; private set; }

    [SerializeField] private Image ballPaintImage;

    private float ballPaintFadingTime = 0.5f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        Ball.Instance.OnCollided += Ball_OnCollided;
    }

    private void Update()
    {
        if (ballPaintImage.color.a > 0f)
        {
            Color color = ballPaintImage.color;
            color.a = Mathf.Max(color.a - Time.deltaTime / ballPaintFadingTime, 0f);
            ballPaintImage.color = color;
        }
    }

    private void Ball_OnCollided(object sender, Ball.OnCollidedEventArgs e)
    {
        if (e.ringPlatformTransform != transform.parent)
            return;
        // Set alpha to 1f (opaque)
        Color color = ballPaintImage.color;
        color.a = 1f;
        ballPaintImage.color = color;

        ballPaintImage.transform.position = e.collidedPosition + Vector3.up * 0.01f;
    }

    private void OnDisable()
    {
        Ball.Instance.OnCollided -= Ball_OnCollided;
    }
}
