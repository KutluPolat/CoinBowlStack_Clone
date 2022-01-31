using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveSystem : MonoBehaviour
{
    #region Variables

    public static int Level 
    { 
        get { return PlayerPrefs.GetInt("Level", 1); } 
        set { PlayerPrefs.SetInt("Level", value); }
    }

    public static int IndexOfLevelInSceneBuild
    {
        get { return Level - 1; }
    }

    public static float Gold
    {
        get { return PlayerPrefs.GetFloat("Gold"); }
        set { PlayerPrefs.SetFloat("Gold", value); }
    }

    #endregion // Variables

    #region Methods


    #region Events

    public static void SubscribeEvents()
    {

    }

    private static void UnsubscribeEvents()
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
