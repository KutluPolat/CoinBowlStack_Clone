using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StackController : MonoBehaviour
{
    private List<GameObject> _stack = new List<GameObject>();
    private GameObject LeaderOfStack 
    { 
        get
        {
            if(_stack.Count == 0)
            {
                return GameManager.Instance.Player;
            }
            else
            {
                return _stack.Last();
            }
        }
    }

    private void AddToStack(GameObject collectedStack)
    {
        _stack.Add(collectedStack);
    }

    private void ConnectCollectedStackToLeader(StackHandler collectedStack)
    {
        collectedStack.ConnectedStack = LeaderOfStack;
    }

    private void SetIsStackedFlagToTrue(StackHandler collectedStack)
    {
        collectedStack.IsStacked = true;
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.ObjectStacked += (GameObject collectedStack) 
            => ConnectCollectedStackToLeader(collectedStack.GetComponent<StackHandler>());

        EventManager.Instance.ObjectStacked += (GameObject collectedStack) 
            => SetIsStackedFlagToTrue(collectedStack.GetComponent<StackHandler>());

        EventManager.Instance.ObjectStacked += AddToStack;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.ObjectStacked -= (GameObject collectedStack)
               => ConnectCollectedStackToLeader(collectedStack.GetComponent<StackHandler>());

        EventManager.Instance.ObjectStacked -= (GameObject collectedStack)
            => SetIsStackedFlagToTrue(collectedStack.GetComponent<StackHandler>());

        EventManager.Instance.ObjectStacked -= AddToStack;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}
