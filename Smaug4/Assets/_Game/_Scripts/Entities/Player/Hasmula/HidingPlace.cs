using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlace : MonoBehaviour
{
    #region Global variables
    [SerializeField] private bool playerColliding = false;
    [SerializeField] private bool keyPressed = false;
    [SerializeField] private bool isHidden = false;
    public GameObject player;

    //Components
    [HideInInspector] public PlayerMagroMov _playerMagroMov;
    private Renderer playerRenderer;
    private Animator _animator;

    //
    private Vector2 _exitPosition;

    #endregion

    #region Unity Functions
    void Start()
    {
        playerRenderer = player.GetComponent<Renderer>();
        _playerMagroMov = player.GetComponent<PlayerMagroMov>();
        _animator = player.GetComponent<Animator>();

        //Posição que o player vai sair do objeto
        _exitPosition = new Vector2(this.transform.position.x, this.transform.position.y - 0.76f);
    }

    void Update()
    {
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
        if (collision.gameObject.CompareTag("Player"))
        {
            playerColliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerColliding = false;
        }
    }
    #endregion

    #region Personal Functions
    public void HidePlayer()
    {
        //some o player
        if (playerColliding == true && keyPressed == true && isHidden == false)
        {
            playerRenderer.enabled = false;
            isHidden = true;
            keyPressed = false;
            _playerMagroMov.CanMove = false;
            _animator.SetFloat("Horizontal", 0f);
            _animator.SetFloat("Vertical", 0f);
            _animator.SetBool("IsWalking", false);
        }
    }

    public void ShowPlayer()
    {
        if (playerColliding == true && keyPressed == true && isHidden == true)
        {
            playerRenderer.enabled = true;
            isHidden = false;
            keyPressed = false;
            player.transform.position = _exitPosition;
            _playerMagroMov.CanMove = true;
            _animator.SetFloat("Horizontal", 0f);
            _animator.SetFloat("Vertical", 0f);
            _animator.SetBool("IsWalking", false);
        }
    }
    #endregion
}
