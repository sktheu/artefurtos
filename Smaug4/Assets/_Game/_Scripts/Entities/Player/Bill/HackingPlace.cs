using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class HackingPlace : MonoBehaviour
{
    #region Vari�veis Globais
    // Inspector:
    [Header("Configura��es:")] 
    [SerializeField] private float disableInterval;
    [SerializeField] private ChangeDevice[] devices;
    
    [Header("Player Input:")]
    [SerializeField] private bool playerColliding = false;
    [SerializeField] private bool keyPressed = false;

    [Header("Sprites")] 
    [SerializeField] private Sprite enableSprite;
    [SerializeField] private Sprite disableSprite;

    // Componentes:
    private SpriteRenderer _spr;
    private PlayerGameOver _playerGameOver;
    private Animator _playerAnimator;

    // Refer�ncias:
    private static CollisionLayersManager _collisionLayersManager;
    private static AudioManager _audioManager;

    private bool _canDisable = true;

    private NavMeshAgent _guardAgent;
    private bool _guardInteracting = false;
    #endregion

    #region Fun��es Unity
    private void Awake()
    {
        _collisionLayersManager = GameObject.FindObjectOfType<CollisionLayersManager>();
        _audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
        _playerGameOver = GameObject.FindObjectOfType<PlayerGameOver>();
        _playerAnimator = _playerGameOver.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (_playerGameOver.GameEnded) return;

        // Input
        if (Input.GetKeyDown(KeyCode.Space))
            keyPressed = true;
        else
            keyPressed = false;

        // Desabilitando
        if (_canDisable && keyPressed && playerColliding)
        {
            _canDisable = false;

            if (devices[devices.Length -1].CanCallGuards)
                DisableDevice();
            else
                DisableDevice(false);

            StartCoroutine(SetDisableInterval(disableInterval));

            if (transform.position.x < _playerAnimator.gameObject.transform.position.x)
                _playerAnimator.SetTrigger("HackingLeft");
            else
                _playerAnimator.SetTrigger("HackingRight");

            _audioManager.PlaySFX("estacao_hack");
        }

        if (_guardAgent != null)
            AnimateGuardInHackingPlace();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _collisionLayersManager.Player.Index)
            playerColliding = true;
        else if (collision.gameObject.layer == _collisionLayersManager.Guards.Index)
            EnableDevice();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _collisionLayersManager.Player.Index)
            playerColliding = false;
    }
    #endregion

    #region Fun��es Pr�prias
    private void DisableDevice(bool canCallGuards=true)
    {
        _spr.sprite = disableSprite;
        foreach (var d in devices)
        {
            d.Disable();
        }

        if (canCallGuards)
        {
            var nearGuard = GuardBehaviour.Guards[0];
            foreach (var g in GuardBehaviour.Guards)
            {
                if (Vector3.Distance(g.transform.position, transform.position) <
                    Vector3.Distance(nearGuard.transform.position, transform.position))
                    nearGuard = g;
            }

            nearGuard.CheckPosition = transform.position;
            nearGuard.SetState(GuardBehaviour.GuardStates.Check);
            _guardAgent = nearGuard.GetComponent<NavMeshAgent>();
        }
    }

    private IEnumerator SetDisableInterval(float t)
    {
        yield return new WaitForSeconds(t);
        EnableDevice();
    }

    private void EnableDevice()
    {
        _audioManager.PlaySFX("estacao_hack");
        foreach (var d in devices)
        {
            if (d.CanCallGuards)
                d.Enable();
        }
        _canDisable = true;
        _spr.sprite = enableSprite;
    }

    private void AnimateGuardInHackingPlace()
    {
        var behaviour = _guardAgent.gameObject.GetComponent<GuardBehaviour>();
        if (_guardAgent.velocity.magnitude == 0f)
        {
            behaviour.IsInHackingPlace = true;
            _guardInteracting = true;
        }
        else if (_guardInteracting)
        {
            _guardAgent.gameObject.GetComponent<Animator>().enabled = true;
            _guardAgent = null;
            _guardInteracting = false;
            behaviour.IsInHackingPlace = false;
        }
    }
    #endregion
}
