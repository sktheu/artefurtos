using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBehaviour : MonoBehaviour
{
    [Header("Configurações:")] 
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float collisionSpeed;
    [SerializeField] private float verticalDirInterval;

    [Header("Sprites:")] 
    [SerializeField] private Sprite[] sprites;

    // Componentes:
    private Rigidbody2D _rb;

    // Movimento Vertical:
    private float _dirY = 1f;
    private bool _canMoveY = true;

    private void Awake() => GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(FlipDirY(verticalDirInterval));
    }

    private void FixedUpdate()
    {
        if (_canMoveY)
            ApplyVerticalMove();
    }

    private void ApplyVerticalMove() => _rb.velocity = Vector2.up * _dirY * verticalSpeed;

    private IEnumerator FlipDirY(float t)
    {
        yield return new WaitForSeconds(t);
        if (_canMoveY)
        {
            _dirY *= -1f;
            StartCoroutine(FlipDirY(verticalDirInterval));
        }
    }
}
