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

    public static int IndexOfLevel
    {
        get { return (Level - 1) % LevelManager.Instance.NumberOfLevels; }
    }

    public static float FillRate
    {
        get { return Mathf.Clamp(PlayerPrefs.GetFloat("FillRate"), 0, 2.73f); }
        set { PlayerPrefs.SetFloat("FillRate", Mathf.Clamp(value, 0, 2.73f)); }
    }

    public static float TotalAssets
    {
        get { return PlayerPrefs.GetFloat("TotalCollectedAssets"); }
        set { PlayerPrefs.SetFloat("TotalCollectedAssets", value); }
    }

    public static float EndingPrizePrice
    {
        get { return EndingPrizePriceMultiplier + EndingPrizePriceMultiplier * HowManyPrizeOpened; }
    }

    public static int HowManyPrizeOpened
    {
        get { return PlayerPrefs.GetInt("HowManyPrizeOpened"); }
        set { PlayerPrefs.SetInt("HowManyPrizeOpened", value); }
    }

    private static float EndingPrizePriceMultiplier = 100;

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
