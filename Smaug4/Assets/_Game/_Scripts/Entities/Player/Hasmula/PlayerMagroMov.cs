using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMagroMov : MonoBehaviour
{
    #region Global variables
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float walkSpeed = 10;
    [SerializeField] private float runSpeed = 20;
    [SerializeField] public bool isWalking = true;
    public bool CanMove = true;


    // Components 
    private Rigidbody2D _rb;
    [HideInInspector] public StaminaSystem _staminaSystem;
    public Animator _anim;
    
    // Movement
    private Vector2 _moveInput;
    

    #endregion


    #region Unity Functions
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _staminaSystem = GetComponent<StaminaSystem>();
      
    }

    private void Update()
    {
        GetMoveInput();
        RunInput();
        
        //Controle de animação
        if (_moveInput.x != 0 || _moveInput.y != 0)
        {
            _anim.SetFloat("Horizontal", _moveInput.x);
            _anim.SetFloat("Vertical", _moveInput.y);
            //_anim.SetFloat("Speed", _moveInput.sqrMagnitude);

            if (_anim.GetBool("IsWalking") == false)
            {
                _anim.SetBool("IsWalking", true);
            }
        }
        else if (_anim.GetBool("IsWalking") == true)
        {
            _anim.SetBool("IsWalking", false);
            StopMoving();
            
        }

    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            ApplyMove();
        }
    }
    #endregion


    #region Personal Functions

    //Aumenta a velocidade quando aperta Shift Esquerdo
    public void RunInput()
    {
        if (_staminaSystem.hasStamina & Input.GetKey(KeyCode.LeftShift) & 
           (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.1f))   
        {
            isWalking = false;
            maxSpeed = runSpeed;
        }
        else
        {
            isWalking = true;
            maxSpeed = walkSpeed;
        }
    }

    /*Velocidade da corrida
    public void SetRunSpeed(float speed)
    {
        maxSpeed = speed;
    }*/

    //Pra para a animação
    private void StopMoving()
    {
        _rb.velocity = Vector2.zero;
    }


    private void GetMoveInput()
    {
        _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }



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


    private void ApplyMove()
    {
        _rb.velocity = SetVelocity(maxSpeed * _moveInput, _rb.velocity, acceleration);
    }

    /*private void FlipSprite()
    {
        if (_moveInput.x == -1f) _spr.flipX = true;
        else if (_moveInput.x == 1f) _spr.flipX = false;
    }*/
    #endregion
}
