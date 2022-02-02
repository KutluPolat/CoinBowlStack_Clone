using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExplosionHandler : MonoBehaviour
{
    private float _explosionForce = 350f, _explosionRadius = 2f;

    public void Explode(BowlHandler bowl)
    {
        foreach (GameObject glassPiece in bowl.GetGlassPieces())
        {
            glassPiece.SetActive(true);
            glassPiece.transform.parent = null;

            Vector3 explosionOffset = Vector3.up * 0.25f;
            Vector3 explosionPosition = bowl.transform.position + explosionOffset;

            glassPiece.AddComponent<Rigidbody>().AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);

            glassPiece.GetComponent<Renderer>().material.DOFade(0, 1f).OnComplete(() => Destroy(glassPiece));
        }

        bowl.DropAllCoins();
    }
}
