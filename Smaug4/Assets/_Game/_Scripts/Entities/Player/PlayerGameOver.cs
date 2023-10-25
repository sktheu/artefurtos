using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameOver : MonoBehaviour
{
    // Componentes:
    private Animator _animator;
    private Rigidbody2D _rb;

    // Referências:
    private CollisionLayersManager _collisionLayersManager;
    private LevelManager _levelManager;
    private AudioManager _audioManager;

    [HideInInspector] public bool GameEnded = false;

    private void Awake()
    {
        _collisionLayersManager = GameObject.FindObjectOfType<CollisionLayersManager>();
        _levelManager = GameObject.FindObjectOfType<LevelManager>();
        //_audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == _collisionLayersManager.Guards.Index)
        {
            if (transform.position.x > col.gameObject.transform.position.x)
                _animator.SetTrigger("GameOverLeft");
            else
                _animator.SetTrigger("GameOverRight");

            GameEnded = true;

            _rb.velocity = Vector2.zero;
            //_audioManager.PlaySFX("game_over");
            _levelManager.Restart();
        }
    }
}
