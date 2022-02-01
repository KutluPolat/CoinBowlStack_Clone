using Coati.CoinBowlStack.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create Coin", menuName = "Coin")]
public class Coin : ScriptableObject
{
    public float Value;
    public CoinType CoinType;
    public Material Material;
    public GameObject Prefab;
}
