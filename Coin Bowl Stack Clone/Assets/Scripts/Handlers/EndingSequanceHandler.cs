using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSequanceHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _endingPrize;

    private Material _material;

    private const float MAX_FILL_RATE = 2.73f, TRANSFER_TIME = 0.05f, WAIT_FOR_CAMERA_BLEND = 2f;
    private float _transferStep;

    private bool IsPrizeReadyToOpen { get { return SaveSystem.TotalAssets >= SaveSystem.EndingPrizePrice; } }

    private void Start()
    {
        _material = new Material(Shader.Find("Shader Graphs/ThreeDSlider"));
        _endingPrize.GetComponent<Renderer>().material = _material;

        InitializePrize();
        SubscribeEvents();
    }

    private void Slide()
    {
        if (GameManager.Instance.TotalAssetInThisLevel > 0)
            StartCoroutine(TransferTotalAssetInThisLevelToTotalAssets());
    }

    private IEnumerator TransferTotalAssetInThisLevelToTotalAssets()
    {
        _transferStep = GameManager.Instance.TotalAssetInThisLevel / 20;

        yield return new WaitForSeconds(WAIT_FOR_CAMERA_BLEND);

        while(GameManager.Instance.TotalAssetInThisLevel > 0)
        {
            TransferAssetsToPrize();

            yield return new WaitForSeconds(TRANSFER_TIME);

            if(IsPrizeReadyToOpen)
            {
                OpenPrize();
            }

            UpdateFillRate();
        }

        EventManager.Instance.OnStateLevelEnd();
    }

    private void TransferAssetsToPrize()
    {
        GameManager.Instance.TotalAssetInThisLevel -= _transferStep;
        SaveSystem.TotalAssets += _transferStep;

        GameManager.Instance.UIController.UpdateTexts();
    }

    private void UpdateFillRate()
    {
        float fillPercentage = SaveSystem.TotalAssets / SaveSystem.EndingPrizePrice;
        SaveSystem.FillRate = fillPercentage * MAX_FILL_RATE;

        _material.SetFloat("_FillRate", SaveSystem.FillRate);
    }

    private void OpenPrize()
    {
        SaveSystem.HowManyPrizeOpened++;
        SaveSystem.TotalAssets = 0;
    }

    private void InitializePrize() => UpdateFillRate();


    private void SubscribeEvents()
    {
        EventManager.Instance.StateEndingSequance += Slide;
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.StateEndingSequance -= Slide;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}
