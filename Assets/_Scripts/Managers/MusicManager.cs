using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string MUSIC_MUTED_KEY = "MusicMuted";

    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.mute = PlayerPrefs.GetInt(MUSIC_MUTED_KEY, 0) == 1;
    }

    public void Mute()
    {
        audioSource.mute = true;
        PlayerPrefs.SetInt(MUSIC_MUTED_KEY, 1);
    }
    public void Unmute()
    {
        audioSource.mute = false;
        PlayerPrefs.SetInt(MUSIC_MUTED_KEY, 0);
    }
    public bool IsMuted()
    {
        return audioSource.mute;
    }
}
