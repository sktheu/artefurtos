using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class ShootBall : MonoBehaviour
{
    #region Vari�veis Globais
    [Header("Bolas:")]
    [SerializeField] private int ballInitialCount;
    [SerializeField] private BallBehaviour ballPrefab;

    [Header("Anima��o:")]
    [SerializeField] private RuntimeAnimatorController shootAnimController;

    [Header("Cursor:")] 
    [SerializeField] private Texture2D aimTexture;

    // Componentes:
    private PlayerMagroMov _playerMove;
    private Animator _animator;
    private Rigidbody2D _rb;
    private PlayerGameOver _playerGameOver;

    // Aiming:
    [HideInInspector] public bool IsAiming;
    private Vector2 _aimDir;

    // Balls:
    private int _ballCurCount;
    private int ballLastCount;
    
    // Animators:
    private RuntimeAnimatorController _defaultAnimController;
    #endregion

    #region Fun��es Unity
    private void Awake() => _ballCurCount = ballInitialCount;

    private void Start()
    {
        DisableCursor();
        _playerMove = GetComponent<PlayerMagroMov>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _playerGameOver = GetComponent<PlayerGameOver>();

        _defaultAnimController = _animator.runtimeAnimatorController;
    }

    private void Update()
    {
        if (HidingPlace.isHidden)
            return;

        if (_playerGameOver.GameEnded || PauseMenu.gamePaused || HidingPlace.isHidden)
        {
            _animator.runtimeAnimatorController = _defaultAnimController;
            IsAiming = false;

            if (!HidingPlace.isHidden)
                _playerMove.CanMove = true;

            if (ballLastCount == 0)
            {
                ballLastCount = _ballCurCount;
                _ballCurCount = 0;
            }
            return;
        }
        

        if (IsAiming)
        {
            SetAimDir();
            AnimateAim();
        }

        if (_ballCurCount > 0 && Input.GetMouseButtonDown(0))
        {
            if (IsAiming)
            {
                DisableCursor();
                AnimateShoot();
                SpawnBall();
                IsAiming = false;
                _playerMove.CanMove = true;
                _ballCurCount--;
            }
            else
            {
                EnableCursor();
                _animator.runtimeAnimatorController = shootAnimController;
                IsAiming = true;
                _playerMove.CanMove = false;
                _rb.velocity = Vector2.zero;
            }
        }

        if (ballLastCount != 0)
        {
            _ballCurCount = ballLastCount;
            ballLastCount = 0;

        }
    }
    #endregion

    #region Fun��es Pr�prias
    private void SetAimDir()
    {
        var mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _aimDir = (mousePos - (Vector2)transform.position).normalized;
    }

    private void SpawnBall()
    {
       var ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
       ball.MoveDir = _aimDir;
    }

    private void AnimateAim()
    {
        _animator.SetFloat("aimX", _aimDir.x);
        _animator.SetFloat("aimY", _aimDir.y);
    }

    private void AnimateShoot()
    {
        if (_aimDir.x > 0 || _aimDir.x > 0 && _aimDir.y != 0)
            _animator.SetTrigger("shootRight");
        else if (_aimDir.x < 0 || _aimDir.x < 0 && _aimDir.y != 0)
            _animator.SetTrigger("shootLeft");
        else if (_aimDir.y > 0)
            _animator.SetTrigger("shootUp");
        else if (_aimDir.y < 0)
            _animator.SetTrigger("shootDown");
    }

    // Animation Event
    private void ResetAnimator() =>_animator.runtimeAnimatorController = _defaultAnimController;

    private void DisableCursor() => Cursor.visible = false;
    
    private void EnableCursor()
    {
        Cursor.SetCursor(aimTexture, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.visible = true;
    }
    #endregion
}
