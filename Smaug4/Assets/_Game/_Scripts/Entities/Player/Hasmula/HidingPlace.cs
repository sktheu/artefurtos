using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlace : MonoBehaviour
{
    #region Global variables
    [SerializeField] private bool playerColliding = false;
    [SerializeField] private bool keyPressed = false;
    [SerializeField] public static bool isHidden = false;
    private GameObject player;

    // References
    private static AudioManager _audioManager;

    // Components
    [HideInInspector] public PlayerMagroMov _playerMagroMov;
    private Rigidbody2D _playerRb;
    private Renderer _playerRenderer;
    private Animator _animator;
    private BoxCollider2D _playerBoxCollider;
    private Animator _HPanimator;
    private PlayerGameOver _playerGameOver;

    private Vector2 _exitPosition;
    #endregion

    #region Unity Functions
    void Start()
    {
        _audioManager = GameObject.FindObjectOfType<AudioManager>();

        //Puxando muitos componentes do player
        isHidden = false;
        player = GameObject.FindWithTag("Player");
        _playerRenderer = player.GetComponent<Renderer>();
        _playerMagroMov = player.GetComponent<PlayerMagroMov>();
        _playerRb = player.GetComponent<Rigidbody2D>(); 
        _playerBoxCollider = player.GetComponent<BoxCollider2D>();
        _animator = player.GetComponent<Animator>();
        _HPanimator = GetComponent<Animator>();
        _playerGameOver = player.GetComponent<PlayerGameOver>();

        //Posição que o player vai sair do objeto
        _exitPosition = new Vector2(transform.position.x, transform.position.y - 0.76f);
    }

    void Update()
    {
        if (_playerGameOver.GameEnded) return;
        
        //detecta quando aperta o espaço
        if (Input.GetKeyDown(KeyCode.Space))
            keyPressed = true;
        else
            keyPressed = false;

        HidePlayer();
        ShowPlayer();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //caso entre na área do armário
        if (collision.gameObject.CompareTag("Player"))
        {
            playerColliding = true;
            Debug.Log("FoiTrigger");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //casó saia da área do armário
        if (collision.gameObject.CompareTag("Player"))
        {
            playerColliding = false;
            Debug.Log("SaiuTrigger");
        }
    }
    #endregion

    #region Personal Functions
    public void HidePlayer()
    {
        //some o player
        if (playerColliding == true && keyPressed == true && isHidden == false)
        {
            foreach (var g in GuardBehaviour.Guards)
                g.SetState(GuardBehaviour.GuardStates.Patrol);

            _playerBoxCollider.enabled = false;
            _playerRenderer.enabled = false;
            isHidden = true;
            keyPressed = false;
            _playerRb.constraints = RigidbodyConstraints2D.FreezePosition;
            _playerMagroMov.CanMove = false;

            _HPanimator.Play("Open");
            _animator.SetFloat("Horizontal", 0f);
            _animator.SetFloat("Vertical", 0f);
            _animator.SetBool("IsWalking", false);
            _audioManager.PlaySFX("armario_abrindo");
        }
    }

    public void ShowPlayer()
    {
        //if (playerColliding == true && keyPressed == true && isHidden == true)
        if (keyPressed == true && isHidden == true)
        {
            _playerBoxCollider.enabled = true;
            _playerRenderer.enabled = true;
            isHidden = false;
            keyPressed = false;
            player.transform.position = _exitPosition;
            _playerMagroMov.CanMove = true;
            _playerRb.constraints = RigidbodyConstraints2D.None;
            _playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;

            _HPanimator.Play("Closing");
            _animator.SetFloat("Horizontal", 0f);
            _animator.SetFloat("Vertical", 0f);
            _animator.SetBool("IsWalking", false);
            _audioManager.PlaySFX("armario_fechando");
        }
    }

    public void IdleInside()
    {
        Debug.Log("foi o evento1");
        _HPanimator.Play("IdleInside");
    }

    public void IdleOutside()
    {
        Debug.Log("foi o evento2");
        _HPanimator.Play("IdleOutside");
    }

    #endregion
}
