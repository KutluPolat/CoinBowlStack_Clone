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
    public delegate void Stack(GameObject collectedStack);
    public delegate void Hit();
    public delegate void Coin();

    #endregion // Delegates

    #region Events

    public event Buttons PressedRestart, PressedNextLevel;
    public event States StateTapToPlay, StateInGame, StateBeginingOfEndingSequance, StateEndingSequance, StateLevelEnd;
    public event InputHandler InputHandled;
    public event Stack ObjectStacked, StackedObjectDestroyed, StackedObjectExchanged;
    public event Hit PlayerHitObstacle;
    public event Coin CoinCollected;

    #endregion // Events

    #region Methods

    public void OnCoinCollected()
    {
        if(CoinCollected != null)
        {
            CoinCollected();

            Debug.Log("CoinCollected triggered.");
        }
    }

    public void OnPlayerHitObstacle()
    {
        if (PlayerHitObstacle != null)
        {
            PlayerHitObstacle();

            Debug.Log("PlayerHitObstacle triggered.");
        }
    }

    public void OnStackedObjectExchanged(GameObject stackedObject)
    {
        if (StackedObjectExchanged != null)
        {
            StackedObjectExchanged(stackedObject);
            Debug.Log("StackedObjectExchanged triggered.");
        }
    }

    public void OnStackedObjectDestroyed(GameObject stackedObject)
    {
        if (StackedObjectDestroyed != null)
        {
            StackedObjectDestroyed(stackedObject);
            Debug.Log("StackedObjectDestroyed triggered.");
        }
    }


    public void OnObjectStacked(GameObject stackedObject)
    {
        if (ObjectStacked != null)
        {
            ObjectStacked(stackedObject);
            Debug.Log("ObjectStacked triggered.");
        }
    }

    public void OnInputHandled(float horizontalInputValue)
    {
        if (InputHandled != null)
        {
            InputHandled(horizontalInputValue);

            Debug.Log("InputHandled triggered.");
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

    public void OnStateEndingSequance()
    {
        if(StateEndingSequance != null)
        {
            StateEndingSequance();

            Debug.Log("StateEndingSequance triggered.");
        }
    }

    public void OnStateBeginningOfEndingSequance()
    {
        if (StateBeginingOfEndingSequance != null)
        {
            StateBeginingOfEndingSequance();

            Debug.Log("StateBeginningOfEndingSequance triggered.");
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

    public void OnStateTapToPlay()
    {
        if (StateTapToPlay != null)
        {
            StateTapToPlay();

            Debug.Log("StateTapToPlay triggered.");
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

    public void OnPressedRestart()
    {
        if (PressedRestart != null)
        {
            PressedRestart();

            Debug.Log("PressedRestart triggered.");
        }
    }

    #endregion // Methods
}
