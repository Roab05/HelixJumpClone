using UnityEngine;

[CreateAssetMenu(fileName = "RingPlatformVisualPrefsSO", menuName = "Scriptable Objects/RingPlatformVisualPrefsSO")]
public class RingPlatformVisualPrefsSO : ScriptableObject
{
    public Transform[] RingPlatformVisualPrefsEasy;
    public Transform[] RingPlatformVisualPrefsMedium;
    public Transform[] RingPlatformVisualPrefsHard;
}
