using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    public static VirtualCamera Instance { get; private set; }

    [SerializeField] private CinemachineFollow cinemachineFollow;
    private Vector3 cinemachineFollowPositionDamping;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        cinemachineFollowPositionDamping = cinemachineFollow.TrackerSettings.PositionDamping;
    }

    private void Start()
    {
        RingPlatformPool.Instance.OnGoalReached += RingPlatformPool_OnGoalReached;
    }

    private void RingPlatformPool_OnGoalReached(object sender, System.EventArgs e)
    {
        StartCoroutine(PositionDampingChange());
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
