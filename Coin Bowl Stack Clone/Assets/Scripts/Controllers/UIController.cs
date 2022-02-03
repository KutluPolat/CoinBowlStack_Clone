using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [BoxGroup("Texts"), SerializeField]
    private TextMeshPro _collectedAssets, _totalAsset;

    [BoxGroup("Texts"), SerializeField]
    private TextMeshProUGUI _totalAssetsInLevel, _level;

    [BoxGroup("Buttons"), SerializeField]
    private Button _restart, _nextLevel;

    private void Start()
    {
        UpdateTexts();
        SubscribeEvents();
    }

    public void UpdateTexts()
    {
        _collectedAssets.text = Mathf.Clamp(GameManager.Instance.CollectedAsset, 0, Mathf.Infinity) + "$";
        _totalAssetsInLevel.text = "Collected: " + Mathf.Clamp(GameManager.Instance.TotalAssetInThisLevel, 0, Mathf.Infinity) + "$";
        _level.text = "Level: " + SaveSystem.Level;

        _totalAsset.text = string.Format("{0:0.00}", SaveSystem.TotalAssets) + "/" + SaveSystem.EndingPrizePrice + "$";
    }

    private void OpenLevelEndUI()
    {
        _restart.gameObject.SetActive(false);
        _nextLevel.gameObject.SetActive(true);
    }

    public void OnRestart() => EventManager.Instance.OnPressedRestart();

    public void OnNextLevel() => EventManager.Instance.OnPressedNextLevel();

    private void SubscribeEvents() 
    {
        EventManager.Instance.StateLevelEnd += OpenLevelEndUI;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.StateLevelEnd -= OpenLevelEndUI;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}
