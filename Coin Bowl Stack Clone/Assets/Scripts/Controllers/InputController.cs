using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private void Start()
    {
        SubscribeEvents();
    }


    #region Variables

    #endregion // Variables

    #region Methods

    #region Events

    private void SubscribeEvents()
    {

    }

    private void UnsubscribeEvents()
    {

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
