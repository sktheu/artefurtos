using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LaserBehaviour : MonoBehaviour
{
    #region Vari�veis Globais
    // Unity Inspector:
    [Header("Din�mico:")]
    [SerializeField] private bool canMove;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 initialMoveDir;

    [Header("Desativar / Ativar:")] 
    [SerializeField] private LaserState initialState;
    [SerializeField] private float enableSpeed;
    [SerializeField] private float disableSpeed;

    [Header("Respawn:")]
    [SerializeField] private Transform respawnPoint;

    // Refer�ncias:
    private static CollisionLayersManager _collisionLayersManager;
    private AudioManager _audioManager;

    // Componentes:
    private Light2D _light;
    private BoxCollider2D _boxCollider;
    private CallGuards _alertScript;

    // Movimenta��o:
    private Vector2 _moveDir;
    private Rigidbody2D _rb;

    // Desablitar / Ativar
    private float _defaultIntensity;
    private bool _canChangeLight;
    private bool _decreasing;

    // SFX:
    private bool _canPlaySfx = true;
    public enum LaserState { On, Off }
    #endregion

    #region Fun��es Unity
    private void Awake()
    {
        _audioManager = GameObject.FindObjectOfType<AudioManager>();
        _collisionLayersManager = GameObject.FindObjectOfType<CollisionLayersManager>();
    }

    private void Start()
    {
        _light = GetComponent<Light2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _alertScript = GetComponent<CallGuards>();
        VerifyInitialState();
        VerifyCanMove();
    }

    private void Update()
    {
        if (_canChangeLight)
            ChangeLight();
    }

    private void FixedUpdate()
    {
        if (canMove)
            ApplyMove();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == _collisionLayersManager.TriggerLaserFlip.Index)
            FlipMoveDirection();
        else if (col.gameObject.layer == _collisionLayersManager.TriggerLaserSpawn.Index)
            RespawnLaser();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == _collisionLayersManager.Player.Index)
        {
            foreach (var g in GuardBehaviour.Guards)
                g.SetState(GuardBehaviour.GuardStates.Chase);

            if (_canPlaySfx)
            {
                _canPlaySfx = false;
                _audioManager.PlaySFX("chamando_guardas");
                StartCoroutine(SetSfxInterval(3f));
            }
        }
    }
    #endregion

    #region Fun��es Pr�prias
    // Movimenta��o
    private void VerifyCanMove()
    {
        if (canMove)
        {
            _rb = GetComponent<Rigidbody2D>();
            _moveDir = initialMoveDir;
        }
        else
        {
            Destroy(GetComponent<Rigidbody2D>());
        }
    }

    private void ApplyMove() => _rb.velocity = _moveDir * moveSpeed;

    private void FlipMoveDirection() => _moveDir *= -1f;

    // Desabilitar / Ativar
    private void VerifyInitialState()
    {
        _defaultIntensity = _light.intensity;

        if (initialState == LaserState.On)
        {
            _light.intensity = 0f;
            _boxCollider.enabled = false;
        }
        
        ChangeState(initialState);
    }
    public void ChangeState(LaserState state)
    {
        if (state == LaserState.Off)
            _decreasing = true;
        else
            _decreasing = false;

        _canChangeLight = true;
    }

    private void ChangeLight()
    {
        if (_decreasing)
        {
            _light.intensity -= disableSpeed * Time.deltaTime;
            if (_light.intensity <= 0f)
            {
                _light.intensity = 0f;
                _canChangeLight = false;
                _boxCollider.enabled = false;
            }
        }
        else
        {
            _light.intensity += enableSpeed * Time.deltaTime;
            if (_light.intensity >= _defaultIntensity)
            {
                _light.intensity = _defaultIntensity;
                _canChangeLight = false;
                _boxCollider.enabled = true;

            }
        }
    }

    public void Stop()
    {
        if (_rb != null)
         _rb.velocity = Vector2.zero;
    }

    private void RespawnLaser()
    {
        canMove = false;
        _rb.velocity = Vector2.zero;
        ChangeState(LaserState.Off);
        Invoke("ResetLaser", 3f);
    }

    private void ResetLaser()
    {
        ChangeState(LaserState.On);
        canMove = true;
        transform.position = respawnPoint.position;
    }

    private IEnumerator SetSfxInterval(float interval)
    {
        yield return new WaitForSeconds(interval);
        _canPlaySfx = true;
    }
    #endregion
}
