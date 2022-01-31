using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private GameObject _destroyStackParticle;

    #endregion

    #region Particles

    /// <summary>
    /// Instantiates a game object attached with particle system
    /// </summary>
    /// <param name="particle">Particle that will spawn.</param>
    /// <param name="spawnPosition">Spawn position of particle</param>
    /// <param name="lifetime">Lifetime of gameobject of particle system</param>
    public void ActivateParticles(ParticleSystem particle, Vector3 spawnPosition, float lifetime = 1f)
    {
        ParticleSystem spawnedParticleObject = Instantiate(particle, spawnPosition, Quaternion.identity);

        Destroy(spawnedParticleObject, lifetime);
    }

    #endregion
}
