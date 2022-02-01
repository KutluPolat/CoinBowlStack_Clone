using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coati.CoinBowlStack.Enums;

public class StackHandler : MonoBehaviour
{
    [HideInInspector]
    public GameObject ConnectedStack;

    [HideInInspector]
    public bool IsStacked;

    [SerializeField]
    private Vector3 _followingOffset = Vector3.forward * 2f;
    private Vector3 TargetPosition { get { return ConnectedStack.transform.position + _followingOffset; } }

    [SerializeField, Range(0, 1f)]
    private float _followingSpeed = 0.25f;

    private void FixedUpdate()
    {
        FollowLeader();
    }

    private void FollowLeader()
    {
        if (IsStacked)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, _followingSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(IsStacked == false)
        {
            Debug.Log("Is stacked false");
            if (other.CompareTag("Stack") || other.CompareTag("Player"))
            {
                Debug.Log("here");
                EventManager.Instance.OnObjectStacked(gameObject);
            }
        }
    }

}
