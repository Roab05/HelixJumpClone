using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    private const string SCORE_TRIGGER_TAG = "ScoreTrigger";
    private const string RING_PLATFORM_TAG = "RingPlatform";
    private const string RING_PLATFORM_ERROR_TAG = "RingPlatformError";

    public static Ball Instance { get; private set; }

    [SerializeField] private Transform ringPlatformGoalTransform;

    public event EventHandler<OnCollidedEventArgs> OnCollided;
    public class OnCollidedEventArgs : EventArgs 
    { 
        public Transform ringPlatformTransform;
        public Vector3 collidedPosition;
    }
    public event EventHandler OnRingPlatformPassed;

    private Rigidbody ballRigidbody;

    [SerializeField] private float jumpVelocity;
    [SerializeField] private float maxFallingVelocity;
    private float startPositionY;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        ballRigidbody = GetComponent<Rigidbody>();
        startPositionY = transform.position.y;
    }
    private void Start()
    {
        RingPlatformPool.Instance.OnGoalReached += RingPlatformPool_OnGoalReached;
    }

    private void RingPlatformPool_OnGoalReached(object sender, EventArgs e)
    {
        transform.position = new Vector3(transform.position.x, startPositionY, transform.position.z);
    }

    private void FixedUpdate()
    {
        LimitBallFallingVelocity();
    }

    private void LimitBallFallingVelocity()
    {
        Vector3 fallingVectorVelocity = ballRigidbody.linearVelocity;

        if (fallingVectorVelocity.y < maxFallingVelocity)
        {
            fallingVectorVelocity.y = maxFallingVelocity;
            ballRigidbody.linearVelocity = fallingVectorVelocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (StreakManager.Instance.IsStreakActive())
        {
            if (collision.transform.parent != ringPlatformGoalTransform)
                NotifyRingPlatformPassed(collision.collider);
        }
        else
        {
            if (collision.gameObject.CompareTag(RING_PLATFORM_ERROR_TAG))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        ballRigidbody.linearVelocity = new Vector3(0f, jumpVelocity, 0f);
        OnCollided?.Invoke(this, new OnCollidedEventArgs
        {
            ringPlatformTransform = collision.transform.parent,
            collidedPosition = collision.contacts[0].point
        });
        StreakManager.Instance.DeactivateStreak();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(SCORE_TRIGGER_TAG))
        {
            NotifyRingPlatformPassed(other);
            StreakManager.Instance.InscreaseScoreStreak();
        }

    }

    private void NotifyRingPlatformPassed(Collider other)
    {
        OnRingPlatformPassed?.Invoke(this, EventArgs.Empty);

        // Notify the RingPlatform that it has been passed
        var ringPlatform = other.GetComponentInParent<RingPlatform>();
        if (ringPlatform != null)
        {
            ringPlatform.SetPassedState();
            ringPlatform.GetRingPlatformVisual().SetTransparent();
        }
    }
    private void OnDestroy()
    {
        RingPlatformPool.Instance.OnGoalReached -= RingPlatformPool_OnGoalReached;
    }
}