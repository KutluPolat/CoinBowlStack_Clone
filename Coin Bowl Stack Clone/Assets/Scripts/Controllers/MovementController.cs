using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coati.CoinBowlStack.Enums;
using DG.Tweening;

public class MovementController : MonoBehaviour
{
    #region Variables
    [SerializeField, Range(0f, 0.3f)]
    private float _horizontalSpeed = 0.001f, _verticalSpeed = 0.12f;

    private MovementState CurrentMovementstate = MovementState.Blocked;

    private bool CanMoveHorizontalAndForward
    {
        get { return CurrentMovementstate == MovementState.HorizontalAndForward; }
    }

    #endregion // Variables

    #region Update
    private void FixedUpdate()
    {
        MoveForward();
    }
    #endregion // Update

    #region Methods
    private void MoveForward()
    {
        if (CanMoveHorizontalAndForward)
        {
            Move(_verticalSpeed * Vector3.forward);
        }
    }

    private void MoveHorizontal(float horizontalInput)
    {
        if (CanMoveHorizontalAndForward && LevelManager.Instance.CurrentGameState == GameState.InGame)
        {
            horizontalInput *= _horizontalSpeed;

            Move(horizontalInput * Vector3.right);
        }
    }

    private void MoveToCenter()
    {
        GameManager.Instance.Player.transform.DOMoveX(0, 1f);
    }

    private void Move(Vector3 input) => GameManager.Instance.PlayerCharacterController.Move(input);

    private void SetMovementStateTo(MovementState state) => CurrentMovementstate = state;

    public void SubscribeEvents()
    {
        EventManager.Instance.StateTapToPlay += () => SetMovementStateTo(MovementState.Blocked);

        EventManager.Instance.StateInGame += () => SetMovementStateTo(MovementState.HorizontalAndForward);

        EventManager.Instance.StateEndingSequance += MoveToCenter;

        EventManager.Instance.StateLevelEnd += () => SetMovementStateTo(MovementState.Blocked);

        EventManager.Instance.InputHandled += MoveHorizontal;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.StateTapToPlay -= () => SetMovementStateTo(MovementState.Blocked);

        EventManager.Instance.StateInGame -= () => SetMovementStateTo(MovementState.HorizontalAndForward);

        EventManager.Instance.StateEndingSequance -= MoveToCenter;

        EventManager.Instance.StateLevelEnd -= () => SetMovementStateTo(MovementState.Blocked);

        EventManager.Instance.InputHandled -= MoveHorizontal;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    #endregion // Methods
}
