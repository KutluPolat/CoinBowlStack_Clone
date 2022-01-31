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

    #endregion // Delegates

    #region Events

    public event Buttons PressedRestart, PressedNextLevel;
    public event States StateTapToPlay, StateInGame, StateEndingSequance, StateLevelEnd;

    #endregion // Events

    #region Methods

    public void OnPressedRestart()
    {
        if (PressedRestart != null)
        {
            PressedRestart();

            Debug.Log("OnPressedRestart triggered.");
        }
    }

    public void OnPressedNextLevel()
    {
        if (PressedNextLevel != null)
        {
            PressedNextLevel();

            Debug.Log("OnPressedNextLevel triggered.");
        }
    }

    public void OnStateTapToPlay()
    {
        if (StateTapToPlay != null)
        {
            StateTapToPlay();

            Debug.Log("OnTapToPlay triggered.");
        }
    }

    public void OnStateInGame()
    {
        if (StateInGame != null)
        {
            StateInGame();
            Debug.Log("OnInGame triggered.");
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
            Debug.Log("OnLevelEnd triggered.");
        }
    }

    #endregion // Methods
}
