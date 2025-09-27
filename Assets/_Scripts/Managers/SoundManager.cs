using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    [SerializeField] private AudioSource audioSource;

    private float tickPitchIncrease = 0.1f;
    private float tickPitchDecrease = 0.2f;
    private float maxTickPitch = 2.4f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.spatialBlend = 0f; // 2D sound

        Ball.Instance.OnCollided += Ball_OnCollided;
        Ball.Instance.OnRingPlatformPassed += Ball_OnRingPlatformPassed;
        Ball.Instance.OnGoalReached += Ball_OnGoalReached;
    }

    private void Ball_OnGoalReached(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.levelUp);
    }

    private void Ball_OnRingPlatformPassed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.tick);
    }

    private void Ball_OnCollided(object sender, Ball.OnCollidedEventArgs e)
    {
        audioSource.Stop();
        PlaySound(audioClipRefsSO.bounce);
    }

    private void PlaySound(AudioClip sound)
    {
        if (sound != audioClipRefsSO.tick)
            ResetPitch();
        audioSource.clip = sound;
        audioSource.Play();
        audioSource.pitch += tickPitchIncrease;
        if (audioSource.pitch > maxTickPitch)
        {
            audioSource.pitch = maxTickPitch - tickPitchDecrease;
        }
    }

    private void ResetPitch()
    {
        audioSource.pitch = 1f;
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
