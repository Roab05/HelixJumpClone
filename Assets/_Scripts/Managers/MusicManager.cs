using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void Mute()
    {
        audioSource.mute = true;
    }
    public void Unmute()
    {
        audioSource.mute = false;
    }
}
