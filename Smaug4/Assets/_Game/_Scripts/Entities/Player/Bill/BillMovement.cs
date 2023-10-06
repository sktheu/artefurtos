using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillMovement : MonoBehaviour
{
    #region Variáveis Globais
    // Inspector:
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;

    // Componentes:
    private Rigidbody2D _rb;
    private Animator _anim;

    // Input:
    private Vector2 _moveInput;
    #endregion

    #region Funções Unity
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        GetMoveInput();
        Animate();
    }

    private void FixedUpdate() => ApplyMove();
    #endregion

    #region Funções Próprias
    private void GetMoveInput() => _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

    private Vector2 SetVelocity(Vector2 goal, Vector2 curVel, float accel)
    {
        var move = Vector2.zero;

        var velDifferenceX = goal.x - curVel.x;
        var velDifferenceY = goal.y - curVel.y;

        if (velDifferenceX > accel) move.x = curVel.x + accel;
        else if (velDifferenceX < -accel) move.x = curVel.x - accel;
        else move.x = goal.x;

        if (velDifferenceY > accel) move.y = curVel.y + accel;
        else if (velDifferenceY < -accel) move.y = curVel.y - accel;
        else move.y = goal.y;

        return move;
    }

    private void ApplyMove() => _rb.velocity = SetVelocity(maxSpeed * _moveInput, _rb.velocity, acceleration);

    private void Animate()
    {
        if (_moveInput.x != 0 || _moveInput.y != 0)
        {
            _anim.SetFloat("Horizontal", _moveInput.x);
            _anim.SetFloat("Vertical", _moveInput.y);

            if (!_anim.GetBool("IsWalking"))
                _anim.SetBool("IsWalking", true);
        }
        else if (_anim.GetBool("IsWalking"))
        {
            _anim.SetBool("IsWalking", false);
        }
    }
    #endregion
}
