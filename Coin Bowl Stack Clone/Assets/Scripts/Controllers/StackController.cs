using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StackController : MonoBehaviour
{
    #region Variables

    [SerializeField, Range(0f, 2f)]
    private float _repairDelay = 1f;

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
        stack.GetComponent<BowlHandler>().RemoveValueFromCollectedAssets();
        RemoveFromStack(stack);
        Destroy(stack);
        StartCoroutine(RepairStacks());
    }

    private void RemoveFromStack(GameObject stack)
    {
        _stacks.Remove(stack);
    }

    private IEnumerator RepairStacks()
    {
        yield return new WaitForSeconds(_repairDelay);

        foreach(GameObject stack in _stacks)
        {
            if (stack.GetComponent<StackHandler>().ConnectedStack == null)
            {
                int currentStackIndex = _stacks.IndexOf(stack);
                bool shouldConnectToPlayer = currentStackIndex == 0;

                GameObject newConnectedStack = shouldConnectToPlayer ? GameManager.Instance.Player : _stacks[currentStackIndex - 1];

                stack.GetComponent<StackHandler>().ConnectedStack = newConnectedStack;
            }
        }
    }

    #endregion // Removing

    #region Collecting

    private void AddToStack(GameObject collectedStack)
    {
        ConnectCollectedStackToLeader(collectedStack.GetComponent<StackHandler>());
        SetIsStackedFlagToTrue(collectedStack.GetComponent<StackHandler>());

        _stacks.Add(collectedStack);
    }

    private void ConnectCollectedStackToLeader(StackHandler collectedStack)
    {
        collectedStack.ConnectedStack = LeaderOfStack;
    }

    private void SetIsStackedFlagToTrue(StackHandler collectedStack)
    {
        collectedStack.IsStacked = true;
    }

    #endregion // Collecting

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
