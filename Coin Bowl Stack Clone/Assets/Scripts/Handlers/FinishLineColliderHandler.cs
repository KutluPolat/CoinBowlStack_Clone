using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineColliderHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(GameObject.FindGameObjectsWithTag("Stack").Length == 0)
            {
                EventManager.Instance.OnStateEndingSequance();
                EventManager.Instance.OnStateLevelEnd();
            }
            else
            {
                EventManager.Instance.OnStateBeginningOfEndingSequance();
            }
        }
    }
}
