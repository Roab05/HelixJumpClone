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
        GameOver
    }
    private State state;

    public event EventHandler OnStateChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;    
        }
        state = State.WaitingToStart;

        Application.targetFrameRate = 90;
    }

    private void Start()
    {
        Ball.Instance.OnDead += Ball_OnDead;

        GameInput.Instance.OnTap += GameInput_OnTap; ;

        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void GameInput_OnTap(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart)
        {
            state = State.Playing;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Ball_OnDead(object sender, EventArgs e)
    {
        state = State.WaitingToStart;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                break;
            case State.Playing:
                break;
            case State.GameOver:
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
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }
}
