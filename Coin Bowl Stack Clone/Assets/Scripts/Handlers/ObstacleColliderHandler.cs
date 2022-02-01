using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleColliderHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stack"))
        {
            EventManager.Instance.OnStackedObjectDestroyed(other.gameObject);
        }
    }
}
