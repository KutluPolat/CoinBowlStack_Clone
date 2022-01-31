using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coati.CoinBowlStack.Enums;

public class InputController : MonoBehaviour
{
    #region Start

    private void Start()
    {

    }

    #endregion // Start

    #region Update
    private void Update()
    {
        HandleInputs();
    }
    #endregion // Update

    #region Variables

    private Vector3 _inputDirection;

    [Range(0.01f, 1f)]
    private readonly float _touchInputSpeed = 0.1f;

    #endregion // Variables

    #region Methods

    #region Input

    private float GetHorizontalInput()
    {
        Vector3 inputHolder = _inputDirection;

        _inputDirection = Vector3.zero;

        return inputHolder.x;
    }

    private void HandleInputs()
    {
#if UNITY_EDITOR
        UnityEditorInputs();
#elif PLATFORM_ANDROID
        AndroidInputs();
#endif

        StartGameIfPlayerSwipes();
    }
    private void UnityEditorInputs()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _inputDirection += -GameManager.Instance.Player.transform.right;
            EventManager.Instance.OnInputHandled(GetHorizontalInput());
        }  
        else if (Input.GetKey(KeyCode.D))
        {
            _inputDirection += GameManager.Instance.Player.transform.right;
            EventManager.Instance.OnInputHandled(GetHorizontalInput());
        }
            
    }

    private void AndroidInputs()
    {
        if (Input.touchCount > 0)
        {
            float horizontalTouchDeltaPosition = Input.touches[0].deltaPosition.x;

            _inputDirection += GameManager.Instance.Player.transform.right * horizontalTouchDeltaPosition * _touchInputSpeed;
            EventManager.Instance.OnInputHandled(GetHorizontalInput());
        }
    }

    private void StartGameIfPlayerSwipes()
    {
        if (_inputDirection != Vector3.zero && LevelManager.Instance.CurrentGameState == GameState.TapToPlay)
            EventManager.Instance.OnStateInGame();
    }

    #endregion // Input

    #endregion // Methods
}
