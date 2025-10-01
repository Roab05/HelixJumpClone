using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalVolume : MonoBehaviour
{
    private Volume volume;
    private DepthOfField depthOfField;

    private void Awake()
    {
        volume = GetComponent<Volume>();

        volume.profile.TryGet(out depthOfField);
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (depthOfField == null)
            return;
        if (!GameManager.Instance.IsWaitingToStart())
        {
            depthOfField.active = false;
        }
        else
        {
            depthOfField.active = true;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }
}
