using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coati.CoinBowlStack.Enums;

public class BowlHandler : MonoBehaviour
{
    [SerializeField]
    private int _maxNumberOfCoinsPerSpawn = 10;

    private float _delayBetweenSpawningCoin = 0.025f;

    private float _valueOfBowl;

    private Stack<GameObject> _coinsInThisStack = new Stack<GameObject>();
    private Stack<CoinType> _coinTypesInThisStack = new Stack<CoinType>();

    [HideInInspector]
    public BowlType BowlSize = BowlType.Small;

    private int MaximumCollectablePackOfCoins { get { return (int)BowlSize + 1; } }
    private bool IsAbleToCollectMoreCoin { get { return _coinTypesInThisStack.Count < MaximumCollectablePackOfCoins; } }

    public void ExchangeCoin()
    {
        GameManager.Instance.CollectedAsset -= _valueOfBowl;
        GameManager.Instance.TotalAsset += _valueOfBowl;
    }

    private void CollectCoin(Coin coin)
    {
        IncreaseValue(coin);
        StartCoroutine(SpawnCoinsInsideOfStack(coin));
    }

    private void IncreaseValue(Coin coin)
    {
        _valueOfBowl += coin.Value;
        GameManager.Instance.CollectedAsset += coin.Value;
    }

    private IEnumerator SpawnCoinsInsideOfStack(Coin coin)
    {
        _coinTypesInThisStack.Push(coin.CoinType);

        for (int i = 0; i < _maxNumberOfCoinsPerSpawn; i++)
        {
            Vector3 randomPositionInsideStack = Random.insideUnitSphere * 0.5f + transform.position;

            GameObject spawnedCoin = Instantiate(coin.Prefab, randomPositionInsideStack, Random.rotation, transform);
            spawnedCoin.GetComponent<Renderer>().material = coin.Material;
            _coinsInThisStack.Push(spawnedCoin);

            yield return new WaitForSeconds(_delayBetweenSpawningCoin);
        }
    }

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
}
