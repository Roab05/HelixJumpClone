using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    private const string SCORE_TRIGGER_TAG = "ScoreTrigger";
    private const string RING_PLATFORM_ERROR_TAG = "RingPlatformError";

    public static Ball Instance { get; private set; }

    [SerializeField] private float jumpVelocity;
    [SerializeField] private float maxFallingVelocity;

    private Rigidbody ballRigidbody;
    private SphereCollider sphereCollider;
    private bool ballCollidedOnStreak = false;

    public event EventHandler<OnCollidedEventArgs> OnCollided;
    public class OnCollidedEventArgs : EventArgs
    {
        public Transform ringPlatformTransform;
        public Vector3 collidedPosition;
    }
    public event EventHandler<OnRingPlatformPassedEventArgs> OnRingPlatformPassed;
    public class OnRingPlatformPassedEventArgs : EventArgs
    {
        public bool ballCollidedOnStreak;
    }
    public event EventHandler OnGoalReached;
    public event EventHandler OnDead;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        ballRigidbody = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();

        ballRigidbody.useGravity = false;
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        RingPlatformPool.Instance.OnPoolReset += RingPlatformPool_OnPoolReset;

        SetBallInitPosition();
    }

    private void RingPlatformPool_OnPoolReset(object sender, EventArgs e)
    {
        SetBallInitPosition();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsPlaying())
        {
            ballRigidbody.useGravity = true;
        }
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
        // if not playing, ignore collisions and bounces
        if (!GameManager.Instance.IsPlaying())
        {
            return;
        }

        var collidedTransform = collision.transform.parent;

        // Ball bounce
        ballRigidbody.linearVelocity = new Vector3(0f, jumpVelocity, 0f);

        if (collidedTransform == RingPlatformPool.Instance.GetRingPlatformGoalTransform())
        {
            // goal reached
            OnGoalReached?.Invoke(this, EventArgs.Empty);

            // Reset ball position
            SetBallInitPosition();

            StreakManager.Instance.DeactivateStreak();
            return;
        }

        if (StreakManager.Instance.IsStreakActive())
        {
            ballCollidedOnStreak = true;
            NotifyRingPlatformPassed(collision.collider);
            OnCollided?.Invoke(this, new OnCollidedEventArgs
            {
                ringPlatformTransform = collision.transform.parent,
                collidedPosition = collision.contacts[0].point
            });

            StartCoroutine(StreakTimeBuffer());
            
            return;
        }

        ballCollidedOnStreak = false;
        // Collide obstacles
        if (collision.gameObject.CompareTag(RING_PLATFORM_ERROR_TAG))
        {
            OnDead?.Invoke(this, EventArgs.Empty);

            SetBallDead();

            StreakManager.Instance.DeactivateStreak();
            return;
        }

        OnCollided?.Invoke(this, new OnCollidedEventArgs
        {
            ringPlatformTransform = collision.transform.parent,
            collidedPosition = collision.contacts[0].point
        });
        StreakManager.Instance.DeactivateStreak();
    }

    private void OnTriggerEnter(Collider other)
    {
        // if not playing, ignore score triggers
        if (!GameManager.Instance.IsPlaying())
        {
            return;
        }

        ballCollidedOnStreak = false;
        if (other.gameObject.CompareTag(SCORE_TRIGGER_TAG))
        {
            NotifyRingPlatformPassed(other);
            StreakManager.Instance.InscreaseScoreStreak();
        }
    }
    private void NotifyRingPlatformPassed(Collider other)
    {
        OnRingPlatformPassed?.Invoke(this, new OnRingPlatformPassedEventArgs 
        { ballCollidedOnStreak = ballCollidedOnStreak });

        // Notify the RingPlatform that it has been passed
        var ringPlatform = other.GetComponentInParent<RingPlatform>();
        if (ringPlatform != null)
        {
            ringPlatform.SetPassedState();
            ringPlatform.GetRingPlatformVisual().SetTransparent();
        }
    }
    private void SetBallDead()
    {
        // free all constraints
        ballRigidbody.constraints = RigidbodyConstraints.None;

        // re-adjust collider
        sphereCollider.radius = 0.1f;
        sphereCollider.center = Vector3.zero;

        // Launch ball at random direction
        float randomAngleFlat = UnityEngine.Random.Range(0f, 360f);
        float launchSpeed = 2f;
        Vector3 randomDirection = new Vector3(Mathf.Cos(randomAngleFlat), 1f, Mathf.Sin(randomAngleFlat)).normalized;
        ballRigidbody.linearVelocity = randomDirection * launchSpeed;
    }
    private void SetBallInitPosition()
    {
        ballRigidbody.position = new Vector3(transform.position.x,
        (int)RingPlatformPool.Instance.GetRingPlatformStartTransform().position.y + 2,
        transform.position.z);
    }

    IEnumerator StreakTimeBuffer()
    {
        yield return new WaitForSeconds(0.2f);
        StreakManager.Instance.DeactivateStreak();
    }
}