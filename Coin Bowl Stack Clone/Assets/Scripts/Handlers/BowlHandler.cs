using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coati.CoinBowlStack.Enums;
using DG.Tweening;
using Sirenix.OdinInspector;

public class BowlHandler : MonoBehaviour
{
    #region Variables

    #region Serialized Fields

    [SerializeField]
    private GameObject _smallVase, _mediumVase, _largeVase;

    [SerializeField]
    private int _maxNumberOfCoinsPerSpawn = 10;

    #endregion // Serialized Fields

    #region Fields

    private float _delayBetweenSpawningCoin = 0.025f;

    private float _valueOfBowl;

    private Stack<GameObject> _coinsInThisStack = new Stack<GameObject>();
    private Stack<CoinType> _coinTypesInThisStack = new Stack<CoinType>();

    [HideInInspector]
    public BowlType BowlSize = BowlType.Small;

    #endregion // Fields

    #region Properties

    private int MaximumCollectablePackOfCoins { get { return (int)BowlSize + 1; } }
    private bool IsAbleToCollectMoreCoin { get { return _coinTypesInThisStack.Count < MaximumCollectablePackOfCoins; } }
    private bool HasToDropCoins { get { return MaximumCollectablePackOfCoins < _coinTypesInThisStack.Count; } }

    #endregion // Properties

    #endregion // Variables

    #region Methods

    #region Size Controls

    public void GetBigger()
    {
        if(BowlSize != BowlType.Large)
        {
            BowlSize++;
        }

        ChooseModelsAccordingToBowlSizes();
    }

    public void GetSmaller()
    {
        if(BowlSize != BowlType.Small)
        {
            BowlSize--;

            if (HasToDropCoins)
            {
                DropExtraCoins();
            }
        }

        ChooseModelsAccordingToBowlSizes();
    }

    private void DropExtraCoins()
    {
        for (int i = 0; i < _maxNumberOfCoinsPerSpawn; i++)
        {
            Vector3 targetDroppingPositionOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            Vector3 randomDroppingPositions = _coinsInThisStack.Peek().transform.position + targetDroppingPositionOffset;

            GameObject coin = _coinsInThisStack.Pop();
            coin.transform.DOJump(randomDroppingPositions, 2f, 1, 2f).OnComplete(() => Destroy(coin));
        }

        switch (_coinTypesInThisStack.Pop())
        {
            case CoinType.OneCent:
                AddValue(-0.5f);
                break;

            case CoinType.FiveCent:
                AddValue(-2.5f);
                break;

            case CoinType.TenCent:
                AddValue(-5f);
                break;
        }
    }

    private void ChooseModelsAccordingToBowlSizes()
    {
        _smallVase.SetActive(false);
        _mediumVase.SetActive(false);
        _largeVase.SetActive(false);

        switch (BowlSize)
        {
            case BowlType.Small:
                _smallVase.SetActive(true);
                break;

            case BowlType.Medium:
                _mediumVase.SetActive(true);
                break;

            case BowlType.Large:
                _largeVase.SetActive(true);
                break;
        }
    }

    #endregion // Size Controls

    #region Value Controls

    public void ExchangeCoin()
    {
        GameManager.Instance.TotalAsset += _valueOfBowl;
        UpdateTextsOnValueChanged();
    }

    public void RemoveValueFromCollectedAssets()
    {
        GameManager.Instance.CollectedAsset -= _valueOfBowl;
        UpdateTextsOnValueChanged();
    }

    private void AddValue(Coin coin)
    {
        AddValue(coin.Value);
    }

    private void AddValue(float value)
    {
        _valueOfBowl += value;
        GameManager.Instance.CollectedAsset += value;

        UpdateTextsOnValueChanged();
    }

    private void UpdateTextsOnValueChanged()
    {
        GameManager.Instance.UIController.UpdateTexts();
    }

    #endregion // Value Controls

    #region Coin Controls

    private void CollectCoin(Coin coin)
    {
        AddValue(coin);
        StartCoroutine(SpawnCoinsInsideOfStack(coin));
    }

    private IEnumerator SpawnCoinsInsideOfStack(Coin coin)
    {
        _coinTypesInThisStack.Push(coin.CoinType);

        for (int i = 0; i < _maxNumberOfCoinsPerSpawn; i++)
        {
            Vector3 defaultYOffset = Vector3.up * 0.25f;
            Vector3 yOffsetAccordingToBowlSize = Vector3.up * 0.4f * (int)BowlSize;

            Vector3 totalYOffset = defaultYOffset + yOffsetAccordingToBowlSize;

            Vector3 randomPositionInsideStack = Random.insideUnitSphere * 0.25f + transform.position + totalYOffset;

            GameObject spawnedCoin = Instantiate(coin.Prefab, randomPositionInsideStack, Random.rotation, transform);
            spawnedCoin.GetComponent<Renderer>().material = coin.Material;
            _coinsInThisStack.Push(spawnedCoin);

            yield return new WaitForSeconds(_delayBetweenSpawningCoin);
        }
    }

    #endregion // Coin Controls

    #region OnTriggerEnter

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Machine"))
        {
            if (IsAbleToCollectMoreCoin)
            {
                CollectCoin(other.GetComponent<MachineHandler>().Coin);
                EventManager.Instance.OnCoinCollected();
            }
        }
    }

    #endregion // OnTriggerEnter

    #endregion // Methods
}
