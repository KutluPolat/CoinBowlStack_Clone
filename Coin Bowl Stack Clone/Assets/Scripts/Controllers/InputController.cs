using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coati.CoinBowlStack.Enums;

public class InputController : MonoBehaviour
{
    #region Variables

    private Vector3 _inputDirection;
    private bool _isInputHandled;

    [Range(0.01f, 1f)]
    private readonly float _touchInputSpeed = 0.1f;

    #endregion // Variables

    #region Updates
    private void Update()
    {
        HandleInputs();
    }

    private void FixedUpdate()
    {
        if (_isInputHandled)
        {
            EventManager.Instance.OnInputHandled(GetHorizontalInput());
            _isInputHandled = false;
        }
    }
    #endregion // Updates

    #region Methods

    #region Input

    private float GetHorizontalInput()
    {
        StartGameIfPlayerSwipes();

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
    }

    private void UnityEditorInputs()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _inputDirection += -GameManager.Instance.Player.transform.right;
            _isInputHandled = true;
        }  
        else if (Input.GetKey(KeyCode.D))
        {
            _inputDirection += GameManager.Instance.Player.transform.right;
            _isInputHandled = true;
        }
            
    }

    private void AndroidInputs()
    {
        if (Input.touchCount > 0)
        {
            float horizontalTouchDeltaPosition = Input.touches[0].deltaPosition.x;

            _inputDirection += GameManager.Instance.Player.transform.right * horizontalTouchDeltaPosition * _touchInputSpeed;
            _isInputHandled = true;
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
