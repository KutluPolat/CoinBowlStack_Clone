using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coati.CoinBowlStack.Enums;
using DG.Tweening;

public class MovementController : MonoBehaviour
{
    #region Variables
    [SerializeField, Range(0.1f, 0.3f)]
    private float _horizontalSpeed = 0.2f, _verticalSpeed = 0.12f;

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
        if (CanMoveHorizontalAndForward)
        {
            horizontalInput *= _horizontalSpeed;

            Move(horizontalInput * Vector3.right);
        }
    }

    private void Move(Vector3 input) => GameManager.Instance.PlayerCharacterController.Move(input);

    private void PushPlayerBack() => StartCoroutine(PushPlayerBackCoroutine());
    private IEnumerator PushPlayerBackCoroutine()
    {
        SetMovementStateTo(MovementState.Backward);

        Vector3 offset = Vector3.back * 3;
        Vector3 endValue = GameManager.Instance.Player.transform.position + offset;

        float jumpPower = 2f;
        int numJumps = 1;
        float duration = 1f;

        GameManager.Instance.Player.transform.DOJump(endValue, jumpPower, numJumps, duration);

        yield return new WaitForSeconds(duration);

        SetMovementStateTo(MovementState.HorizontalAndForward);
        AnimationManager.Instance.ActivateAnimation_PushedBack();
    }

    private void SetMovementStateTo(MovementState state) => CurrentMovementstate = state;

    public void SubscribeEvents()
    {
        EventManager.Instance.StateTapToPlay += () => SetMovementStateTo(MovementState.Blocked);
        EventManager.Instance.StateInGame += () => SetMovementStateTo(MovementState.HorizontalAndForward);
        EventManager.Instance.StateEndingSequance += () => SetMovementStateTo(MovementState.Blocked);

        EventManager.Instance.InputHandled += MoveHorizontal;

        EventManager.Instance.PlayerHitObstacle += PushPlayerBack;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.StateTapToPlay -= () => SetMovementStateTo(MovementState.Blocked);
        EventManager.Instance.StateInGame -= () => SetMovementStateTo(MovementState.HorizontalAndForward);
        EventManager.Instance.StateEndingSequance -= () => SetMovementStateTo(MovementState.Blocked);

        EventManager.Instance.InputHandled += MoveHorizontal;

        EventManager.Instance.PlayerHitObstacle -= PushPlayerBack;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    #endregion // Methods
}
