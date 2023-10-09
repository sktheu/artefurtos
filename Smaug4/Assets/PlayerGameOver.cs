using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameOver : MonoBehaviour
{
    // Componentes:


    // Referências:
    private CollisionLayersManager _collisionLayersManager;

    [HideInInspector] public bool GameEnded = false;

    private void Awake() => _collisionLayersManager = GameObject.FindObjectOfType<CollisionLayersManager>();

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == _collisionLayersManager.Guards.Index)
        {
            GameEnded = true;

        }
    }
}
