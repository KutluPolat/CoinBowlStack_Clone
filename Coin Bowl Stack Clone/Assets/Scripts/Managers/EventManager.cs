using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Singleton

    public static EventManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion // Singleton

    #region Delegates

    public delegate void Buttons();
    public delegate void States();
    public delegate void InputHandler(float horizontalInputValue);
    public delegate void Hit();

    #endregion // Delegates

    #region Events

    public event Buttons PressedRestart, PressedNextLevel;
    public event States StateTapToPlay, StateInGame, StateEndingSequance, StateLevelEnd;
    public event InputHandler InputHandled;
    public event Hit PlayerHitObstacle;

    #endregion // Events

    #region Methods

    public void OnPressedRestart()
    {
        if (PressedRestart != null)
        {
            PressedRestart();

            Debug.Log("PressedRestart triggered.");
        }
    }

    public void OnPressedNextLevel()
    {
        if (PressedNextLevel != null)
        {
            PressedNextLevel();

            Debug.Log("PressedNextLevel triggered.");
        }
    }

    public void OnStateTapToPlay()
    {
        if (StateTapToPlay != null)
        {
            StateTapToPlay();

            Debug.Log("StateTapToPlay triggered.");
        }
    }

    public void OnStateInGame()
    {
        if (StateInGame != null)
        {
            StateInGame();
            Debug.Log("StateInGame triggered.");
        }
    }

    public void OnStateEndingSequance()
    {
        if (StateEndingSequance != null)
        {
            StateEndingSequance();

            Debug.Log("StateEndingSequance triggered.");
        }
    }

    public void OnStateLevelEnd()
    {
        if (StateLevelEnd != null)
        {
            StateLevelEnd();

            Debug.Log("StateLevelEnd triggered.");
        }
    }

    public void OnInputHandled(float horizontalInputValue)
    {
        if(InputHandled != null)
        {
            InputHandled(horizontalInputValue);

            Debug.Log("InputHandled triggered.");
        }
    }

    public void OnPlayerHitObstacle()
    {
        if(PlayerHitObstacle != null)
        {
            PlayerHitObstacle();

            Debug.Log("PlayerHitObstacle triggered.");
        }
    }

    #endregion // Methods
}
