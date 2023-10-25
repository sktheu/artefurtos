using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    #region Variáveis Globais
    [Header("Configurações:")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float checkDistance;

    // Referências:
    private static CollisionLayersManager _collisionLayersManager;
    private static AudioManager _audioManager;

    // Componentes:
    private Rigidbody2D _rb;

    // Movimento:
    [HideInInspector] public Vector2 MoveDir;
    #endregion

    #region Funções Unity
    private void Awake()
    {
        _collisionLayersManager = GameObject.FindObjectOfType<CollisionLayersManager>();
        //_audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    private void Start() => _rb = GetComponent<Rigidbody2D>();

    private void FixedUpdate()
    {
        Rotate();
        ApplyMove();
    }
    #endregion

    #region Funções Próprias
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer != _collisionLayersManager.Player.Index)
        {
            if (col.gameObject.layer == _collisionLayersManager.Guards.Index)
            {
                // TODO: Nocautear guarda?
            }
            else
            {
                CheckGuards();
                //_audioManager.PlaySFX("bola_colisao");
                Destroy(gameObject, 0.5f);
            }
        }
    }

    private void Rotate() => transform.Rotate(Vector3.forward * rotateSpeed);

    private void ApplyMove() => _rb.velocity = MoveDir * moveSpeed;

    private void CheckGuards()
    {
        foreach (var g in GuardBehaviour.Guards)
        {
            if (Vector2.Distance(g.transform.position, transform.position) <= checkDistance)
            {
                g.CheckPosition = transform.position;
                g.SetState(GuardBehaviour.GuardStates.Check);
            }
        } 
    }
    #endregion
}
