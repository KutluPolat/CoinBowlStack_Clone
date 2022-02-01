using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coati.CoinBowlStack.Enums;

public class MachineHandler : MonoBehaviour
{
    public Coin Coin;

    [SerializeField]
    private List<GameObject> _particleSystems = new List<GameObject>();

    private void Start()
    {
        InitializeParticleSystems();
    }

    private void InitializeParticleSystems()
    {
        foreach (GameObject particleSystem in _particleSystems)
            particleSystem.GetComponent<Renderer>().material = Coin.Material;
    }
}
