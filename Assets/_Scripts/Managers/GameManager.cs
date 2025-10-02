using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public enum State
    {
        WaitingToStart,
        Playing,
        BallDead,
        GameOver,
        Crossfade
    }
    private State state;

    public event EventHandler OnStateChanged;

    private float gameOverTime = 2f;
    private float crossfadeTime = 0.4f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;    
        }
        state = State.WaitingToStart;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

    }
    private void Start()
    {
        Ball.Instance.OnDead += Ball_OnDead;
        GameInput.Instance.OnTap += GameInput_OnTap;

        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void GameInput_OnTap(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart)
        {
            state = State.Playing;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
        else if (state == State.GameOver)
        {
            state = State.Crossfade;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Ball_OnDead(object sender, EventArgs e)
    {
        state = State.BallDead;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                break;
            case State.Playing:
                break;
            case State.BallDead:
                gameOverTime -= Time.deltaTime;
                if (gameOverTime <= 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
            case State.Crossfade:
                crossfadeTime -= Time.deltaTime;
                if (crossfadeTime <= 0f)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                break;
        }
    }

    public bool IsWaitingToStart()
    {
        return state == State.WaitingToStart;
    }
    public bool IsPlaying()
    {
        return state == State.Playing;
    }
    public bool IsBallDead()
    {
        return state == State.BallDead;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }
    public bool IsCrossfade()
    {
        return state == State.Crossfade;
    }
}
