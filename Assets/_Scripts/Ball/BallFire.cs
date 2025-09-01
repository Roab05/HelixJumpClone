using UnityEngine;

public class BallFire : MonoBehaviour
{
    private ParticleSystem ballFire;

    private void Awake()
    {
        ballFire = GetComponent<ParticleSystem>();
    }
    private void Start()
    {
        StreakManager.Instance.OnStreakChanged += StreakManager_OnStreakChanged;
    }

    private void StreakManager_OnStreakChanged(object sender, StreakManager.OnStreakChangedEventArgs e)
    {
        if (e.isStreakActive)
        {
            ballFire.Play();
        }
        else
        {
            ballFire.Stop();
        }
    }
}
