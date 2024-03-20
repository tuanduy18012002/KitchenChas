using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    public event EventHandler OnPausedGame;
    public event EventHandler OnUnPausedGame;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public static KitchenGameManager Instance { get; private set; }


    private State state;
    private float timer;
    private float countdownTimerMax;
    private float playingTimerMax;
    private bool isPaused;

    private void Awake()
    {
        Instance = this;
        countdownTimerMax = 3f;
        playingTimerMax = 30f;
        isPaused = false;
    }

    private void Start()
    {
        state = State.WaitingToStart;
        InputManager.Instance.OnPausePressed += InputManager_OnPausePressed;
        InputManager.Instance.OnTutorialEnd += InputManager_OnTutorialEnd;
    }

    private void InputManager_OnTutorialEnd(object sender, EventArgs e)
    {
        timer = countdownTimerMax;
        state = State.CountdownToStart;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs() { state = state });
    }

    private void InputManager_OnPausePressed(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch(state) 
        { 
            case State.WaitingToStart: 
                break;
            case State.CountdownToStart:
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    timer = playingTimerMax;
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs() { state = state });
                }
                break;
            case State.GamePlaying:
                timer -= Time.deltaTime;
                if (timer <= 0) 
                {
                    timer = 0;
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs() { state = state });
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public float GetTimer()
    {
        return timer;
    }

    public bool IsGamePlaying() { return state == State.GamePlaying; }

    public bool IsWaitingToStart() { return state == State.WaitingToStart; }

    public float GetPlayingTimerNormalize()
    {
        return 1 - timer / playingTimerMax;
    }

    public void TogglePauseGame()
    {
        if (OptionsUI.Instance.IsShow())
        {
            OptionsUI.Instance.Hide();
        }

        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            OnPausedGame?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnUnPausedGame?.Invoke(this, EventArgs.Empty);
        }
    }
}
