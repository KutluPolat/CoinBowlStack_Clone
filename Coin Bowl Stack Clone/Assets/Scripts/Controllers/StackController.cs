using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class StackController : MonoBehaviour
{
    #region Variables
    private float _waveScalingSpeed = 0.15f, _delayBetweenWaves = 0.05f;

    [SerializeField, Range(0f, 5f)]
    private float _jumpDuration = 2f;

    private List<GameObject> _stacks = new List<GameObject>();
    private GameObject LeaderOfStack 
    { 
        get
        {
            if(_stacks.Count == 0)
            {
                return GameManager.Instance.Player;
            }
            else
            {
                return _stacks.Last();
            }
        }
    }

    #endregion // Variables

    #region Methods

    #region Exchanging

    private void ExchangeStack(GameObject stack)
    {
        stack.GetComponent<BowlHandler>().ExchangeCoin();
        DestroyStackedObject(stack);

        if(_stacks.Count == 0)
        {
            EventManager.Instance.OnStateLevelEnd();
        }
    }

    #endregion // Exchanging

    #region Destroying

    private void DestroyStackedObject(GameObject stack)
    {
        GameManager.Instance.ExplosionHandler.Explode(stack.GetComponent<BowlHandler>());
        ThrowStackedObjectsAway(stack);
        RemoveFromStack(stack);
        Destroy(stack);
    }

    private void RemoveFromStack(GameObject stack)
    {
        stack.GetComponent<BowlHandler>().RemoveValueFromCollectedAssets();

        stack.GetComponent<StackHandler>().ConnectedStack = null;
        stack.GetComponent<StackHandler>().IsStacked = false;

        _stacks.Remove(stack);
    }

    private void ThrowStackedObjectsAway(GameObject stack)
    {
        for (int i = _stacks.Count - 1; i > _stacks.IndexOf(stack); i--)
        {
            Vector3 randomJumpOffset = new Vector3(Random.Range(-4f, 4f), 0, Random.Range(10f, 15f));
            Vector3 finalJumpPosition = _stacks[i].transform.position + randomJumpOffset;
            finalJumpPosition.x = Mathf.Clamp(finalJumpPosition.x, -4f, 4f);

            GameObject throwedStack = _stacks[i];
            RemoveFromStack(_stacks[i]);

            throwedStack.transform.DOJump(finalJumpPosition, 1f, 0, _jumpDuration);
        }
    }

    #endregion // Removing

    #region Collecting

    private void AddToStack(GameObject collectedStack)
    {
        AddValueOfCollectedStackToCollectedAssets(collectedStack.GetComponent<BowlHandler>());
        ConnectCollectedStackToLeader(collectedStack.GetComponent<StackHandler>());
        SetIsStackedFlagToTrue(collectedStack.GetComponent<StackHandler>());

        _stacks.Add(collectedStack);
        StartCoroutine(WaveEffect());
    }

    private void AddValueOfCollectedStackToCollectedAssets(BowlHandler collectedStack) => collectedStack.AddValueOfThisToCollectedAssets();
    private void ConnectCollectedStackToLeader(StackHandler collectedStack) => collectedStack.ConnectedStack = LeaderOfStack;
    private void SetIsStackedFlagToTrue(StackHandler collectedStack) => collectedStack.IsStacked = true;

    #endregion // Collecting

    #region Wave Effect

    private IEnumerator WaveEffect()
    {
        for (int i = _stacks.Count - 1; i >= 0; i--)
        {
            _stacks[i].transform.DOScale(1.2f, _waveScalingSpeed);

            yield return new WaitForSeconds(_delayBetweenWaves);

            _stacks[i].transform.DOScale(1f, _waveScalingSpeed);
        }
    }

    #endregion // Wave Effect

    #region Events

    public void SubscribeEvents()
    {
        EventManager.Instance.ObjectStacked += AddToStack;

        EventManager.Instance.StackedObjectDestroyed += DestroyStackedObject;

        EventManager.Instance.StackedObjectExchanged += ExchangeStack;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.ObjectStacked -= AddToStack;

        EventManager.Instance.StackedObjectDestroyed -= DestroyStackedObject;

        EventManager.Instance.StackedObjectExchanged -= ExchangeStack;
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
