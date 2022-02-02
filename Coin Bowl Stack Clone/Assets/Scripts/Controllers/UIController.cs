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
        UpdateTexts();
    }

    public void UpdateTexts()
    {
        _collectedAssets.text = GameManager.Instance.CollectedAsset.ToString() + "$";
        _totalAssets.text = GameManager.Instance.TotalAsset.ToString() + "$";
    }
}
