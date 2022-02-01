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
        UpdateTotalAssetText();
    }

    private void UpdateTotalAssetText() => _collectedAssets.text = GameManager.Instance.CollectedAsset.ToString() + "$";

    private void SubscribeEvents()
    {
        EventManager.Instance.CoinCollected += UpdateTotalAssetText;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.CoinCollected -= UpdateTotalAssetText;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}
