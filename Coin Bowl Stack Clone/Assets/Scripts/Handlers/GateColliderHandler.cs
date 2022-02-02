using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coati.CoinBowlStack.Enums;

public class GateColliderHandler : MonoBehaviour
{
    [SerializeField]
    private GateType _gateType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stack"))
        {
            switch (_gateType)
            {
                case GateType.Smaller:
                    other.GetComponent<BowlHandler>().GetSmaller();
                    break;

                case GateType.Bigger:
                    other.GetComponent<BowlHandler>().GetBigger();
                    break;
            }
        }
    }
}
