using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    [SerializeField] private AudioSource tickAudioSource;

    private float tickPitchIncrease = 0.1f;
    private float tickPitchDecrease = 0.2f;
    private float maxTickPitch = 2.4f;

    private void Start()
    {
        tickAudioSource.clip = audioClipRefsSO.tick;
        tickAudioSource.volume = 0.5f;
        tickAudioSource.spatialBlend = 0f; // 2D sound

        Ball.Instance.OnCollided += Ball_OnCollided;
        Ball.Instance.OnRingPlatformPassed += Ball_OnRingPlatformPassed;
        RingPlatformPool.Instance.OnGoalReached += RingPlatformPool_OnGoalReached;
    }

    private void RingPlatformPool_OnGoalReached(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(audioClipRefsSO.levelUp, Camera.main.transform.position);
    }

    private void Ball_OnRingPlatformPassed(object sender, System.EventArgs e)
    {
        PlayTickSound();
    }

    private void Ball_OnCollided(object sender, Ball.OnCollidedEventArgs e)
    {
        tickAudioSource.Stop();
        ResetTickPitch();
        AudioSource.PlayClipAtPoint(audioClipRefsSO.bounce, Camera.main.transform.position, 0.2f);
    }

    private void PlayTickSound()
    {
        tickAudioSource.Play();
        tickAudioSource.pitch += tickPitchIncrease;
        if (tickAudioSource.pitch > maxTickPitch)
        {
            tickAudioSource.pitch = maxTickPitch - tickPitchDecrease;
        }
    }

    private void ResetTickPitch()
    {
        if (tickAudioSource.pitch > 1f)
            tickAudioSource.pitch = 1f;
    }
}
