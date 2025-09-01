using UnityEngine;

public class BallSplash : MonoBehaviour
{
    private ParticleSystem ballSplash;

    private void Awake()
    {
        ballSplash = GetComponent<ParticleSystem>();
    }
    private void Start()
    {
        Ball.Instance.OnCollided += Ball_OnCollided;
    }

    private void Ball_OnCollided(object sender, Ball.OnCollidedEventArgs e)
    {
        ballSplash.Play();
    }
}
