using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coati.CoinBowlStack.Enums;

public class AnimationManager : MonoBehaviour
{
    #region Singleton

    public static AnimationManager Instance { get; private set; }

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

        SubscribeEvents();
    }

    #endregion // Singleton

    #region Variables

    #region Animations

    [SerializeField]
    private float _animationTransitionSpeed = 0.1f;

    private float _currentHorizontal, _currentVertical;


    #endregion // Animations

    #endregion // Variables

    #region Methods

    #region PlayerAnimation

    #region ActivateAnimation

    public void ActivateAnimation_GoTowardLeft()
    {
        AddToHorizontal(_animationTransitionSpeed * -1);
        GameManager.Instance.PlayerAnimator.SetFloat("Horizontal", _currentHorizontal);
    }

    public void ActivateAnimation_GoTowardRight()
    {
        AddToHorizontal(_animationTransitionSpeed);
        GameManager.Instance.PlayerAnimator.SetFloat("Horizontal", _currentHorizontal);
    }

    private void AddToHorizontal(float value) => _currentHorizontal += value;

    public void ActivateAnimation_PushedBack()
    {

    }

    #endregion // ActivateAnimation

    #endregion // PlayerAnimation

    #region Camera Animations

    private void CameraConditionOne() => GameManager.Instance.CameraAnimator.Play("ConditionOne");
    private void CameraConditionTwo() => GameManager.Instance.CameraAnimator.Play("ConditionTwo");

    #endregion // Camera Animations

    #region Events

    private void SubscribeEvents()
    {
        EventManager.Instance.StateTapToPlay += CameraConditionOne;
        EventManager.Instance.StateLevelEnd += CameraConditionTwo;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.StateTapToPlay -= CameraConditionOne;
        EventManager.Instance.StateLevelEnd -= CameraConditionTwo;
    }

    #endregion // Events

    #region OnDestroy

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    #endregion // OnDestroy

    #endregion // Methods
}
