using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBehaviour : MonoBehaviour
{
    #region Variáveis Globais
    [Header("Configurações:")]
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float verticalDirInterval;

    [Header("Sprites:")] 
    [SerializeField] private Sprite[] sprites;

    // Referências:
    private CollisionLayersManager _collisionLayersManager;

    // Componentes:
    private Rigidbody2D _rb;

    // Movimento Vertical:
    private float _dirY = 1f;
    private bool _canMoveY = true;

    // Coleta:
    private bool _hasCollided = false;
    private static Transform _playerTransform;
    [SerializeField] private PlayerObjective _playerObjective;
    #endregion

    #region Funções Unity

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        _collisionLayersManager = GameObject.FindObjectOfType<CollisionLayersManager>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _playerObjective = _playerTransform.gameObject.GetComponent<PlayerObjective>();
    } 

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(FlipDirY(verticalDirInterval));
    }

    private void FixedUpdate()
    {
        if (_canMoveY)
            ApplyVerticalMove();

        if (_hasCollided)
            ChasePlayer();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == _collisionLayersManager.Player.Index)
        {
            _playerObjective.AddTreasure(GetComponent<SpriteRenderer>().sprite);
            _canMoveY = false;
            _hasCollided = true;
            transform.localScale = transform.localScale * 0.5f;
            Destroy(gameObject, 0.2f);
        }
    }
    #endregion

    #region Funções Próprias
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

    private void ChasePlayer()
    {
        var dir = new Vector2(_playerTransform.transform.position.x - transform.position.x,
            _playerTransform.position.y - transform.position.y).normalized;
        _rb.velocity = dir * chaseSpeed;
    }
    #endregion
}
