using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class UIController : MonoBehaviour
{
    [BoxGroup("Texts"), SerializeField]
    private TextMeshPro _collectedAssets;

    [BoxGroup("Texts"), SerializeField]
    private TextMeshProUGUI _totalAssets;


    private void Start()
    {
        SubscribeEvents();
        UpdateTexts(null);
    }

    private void UpdateCollectedAssetsText() => _collectedAssets.text = GameManager.Instance.CollectedAsset.ToString() + "$";
    private void UpdateTexts(GameObject stack)
    {
        UpdateCollectedAssetsText();
        _totalAssets.text = GameManager.Instance.TotalAsset.ToString() + "$";
    }

    private void SubscribeEvents()
    {
        EventManager.Instance.CoinCollected += UpdateCollectedAssetsText;

        EventManager.Instance.StackedObjectExchanged += UpdateTexts;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.CoinCollected -= UpdateCollectedAssetsText;

        EventManager.Instance.StackedObjectExchanged -= UpdateTexts;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}
