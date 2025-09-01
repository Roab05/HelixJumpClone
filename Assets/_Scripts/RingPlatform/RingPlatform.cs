using Unity.VisualScripting;
using UnityEngine;

public class RingPlatform : MonoBehaviour
{

    private bool isMoveUp = false;

    private float moveUpDuration = 1f;
    private float velocityY = 0f;
    readonly private float moveUpAcceleration = 10f;
    private float startPositionY;

    private BoxCollider boxTrigger;
    private RingPlatformVisual ringPlatformVisual;

    private void Start()
    {
        ringPlatformVisual = GetComponentInChildren<RingPlatformVisual>();
        boxTrigger = GetComponentInChildren<BoxCollider>();

        startPositionY = transform.position.y;
        RingPlatformPool.Instance.OnGoalReached += RingPlatformPool_OnGoalReached;
    }

    private void RingPlatformPool_OnGoalReached(object sender, System.EventArgs e)
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
    }

    private void OnDestroy()
    {
        RingPlatformPool.Instance.OnGoalReached -= RingPlatformPool_OnGoalReached;
    }
}