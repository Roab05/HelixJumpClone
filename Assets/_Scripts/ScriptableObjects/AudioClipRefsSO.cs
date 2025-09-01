using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipRefsSO", menuName = "Scriptable Objects/AudioClipRefsSO")]
public class AudioClipRefsSO : ScriptableObject
{
    [SerializeField] public AudioClip bounce;
    [SerializeField] public AudioClip tick;
    [SerializeField] public AudioClip levelUp;
    
}
