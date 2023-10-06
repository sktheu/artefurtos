using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    // Componentes:
    private Rigidbody2D _rb;

    // Movimentação
    private Vector2 _moveDir;

    private void Start() => _rb = GetComponent<Rigidbody2D>();

    private void Update() => _moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    
    private void FixedUpdate() => _rb.velocity = _moveDir * moveSpeed;
}
