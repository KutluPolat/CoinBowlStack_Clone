using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeOfficeColliderHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stack"))
        {
            EventManager.Instance.OnStackedObjectExchanged(other.gameObject);
        }
    }
}
