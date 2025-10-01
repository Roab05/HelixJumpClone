using Unity.VisualScripting;
using UnityEngine;

public class RingPlatform : MonoBehaviour
{

    private bool isMoveUp = false;

    private float moveUpDuration = 1f;
    private float velocityY = 0f;
    readonly private float moveUpAcceleration = 10f;
    private float startPositionY;
    private int spinDir;

    private BoxCollider boxTrigger;
    private RingPlatformVisual ringPlatformVisual;

    private void Awake()
    {
        ringPlatformVisual = GetComponentInChildren<RingPlatformVisual>();
        boxTrigger = GetComponentInChildren<BoxCollider>();
    }
    private void Start()
    {
        startPositionY = transform.position.y;

        Ball.Instance.OnGoalReached += Ball_OnGoalReached;

        SetInitialState();
    }

    private void Ball_OnGoalReached(object sender, System.EventArgs e)
    {
        SetInitialState();
    }


    private void Update()
    {
        if (isMoveUp)
        {
            if (moveUpDuration <= 0f)
            {
                SetInitialState();
                gameObject.SetActive(false);
            }
            moveUpDuration -= Time.deltaTime;
            velocityY += moveUpAcceleration * Time.deltaTime;
            transform.Translate(Time.deltaTime * velocityY * Vector3.up);
        }
        transform.Rotate(0f, DifficultyManager.Instance.GetPlatformSpinSpeed() * Time.deltaTime * spinDir, 0f);
    }

    public void SetPassedState()
    {
        isMoveUp = true;
        boxTrigger.enabled = false;
    }

    public RingPlatformVisual GetRingPlatformVisual()
    {
        return ringPlatformVisual;
    }

    public void SetNewVisual(RingPlatformVisual ringPlatformVisual)
    {
        if (this.ringPlatformVisual != null)
        {
            Destroy(this.ringPlatformVisual.gameObject);
        }
        this.ringPlatformVisual = ringPlatformVisual;
    }

    private void SetInitialState()
    {
        moveUpDuration = 1f;
        velocityY = 0f;
        boxTrigger.enabled = true;
        isMoveUp = false;
        transform.position = new Vector3(transform.position.x, startPositionY, transform.position.z);
        spinDir = GetSpinDir();
    }

    private int GetSpinDir()
    {
        var rand = Random.Range(1, Mathf.Max(3, 23 - LevelManager.Instance.GetLevel()));
        if (rand == 2)
            return Random.Range(0, 2) == 0 ? -1 : 1;
        return 0;
    }
    private void OnDisable()
    {
        Ball.Instance.OnGoalReached -= Ball_OnGoalReached;
    }
}