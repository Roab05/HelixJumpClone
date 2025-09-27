using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    public static VirtualCamera Instance { get; private set; }

    [SerializeField] private CinemachineFollow cinemachineFollow;
    [SerializeField] private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private Vector3 cinemachineFollowPositionDamping;
    private Vector3 playingEulerAngles = new Vector3(34.5f, 0f, 0f);
    private Vector3 targetEulerAngles = new Vector3(16f, -8f, 0f);

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        cinemachineFollowPositionDamping = cinemachineFollow.TrackerSettings.PositionDamping;
    }

    private void Start()
    {
        transform.eulerAngles = targetEulerAngles;
        Ball.Instance.OnGoalReached += Ball_OnGoalReached;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsPlaying())
        {
            targetEulerAngles = playingEulerAngles;
        }
        cinemachineFollow.enabled = GameManager.Instance.IsPlaying();
        cinemachineBasicMultiChannelPerlin.enabled = GameManager.Instance.IsWaitingToStart();
    }

    private void Ball_OnGoalReached(object sender, System.EventArgs e)
    {
        StartCoroutine(PositionDampingChange());
    }

    private void Update()
    {
        float lerpSpeed = 4f;

        Vector3 currentAngles = transform.eulerAngles;
        currentAngles.x = Mathf.LerpAngle(currentAngles.x, targetEulerAngles.x, Time.deltaTime * lerpSpeed);
        currentAngles.y = Mathf.LerpAngle(currentAngles.y, targetEulerAngles.y, Time.deltaTime * lerpSpeed);
        currentAngles.z = Mathf.LerpAngle(currentAngles.z, targetEulerAngles.z, Time.deltaTime * lerpSpeed);

        transform.eulerAngles = currentAngles;
    }

    IEnumerator PositionDampingChange()
    {
        TurnOffPositionDamping();

        yield return null; // Wait 1 frame

        TurnOnPositionDamping();
    }
    public void TurnOffPositionDamping()
    {
        cinemachineFollow.TrackerSettings.PositionDamping = Vector3.zero;
    }

    public void TurnOnPositionDamping()
    {
        cinemachineFollow.TrackerSettings.PositionDamping = cinemachineFollowPositionDamping;
    }
}
