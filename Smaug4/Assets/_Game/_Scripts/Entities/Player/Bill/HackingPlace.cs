using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HackingPlace : MonoBehaviour
{
    #region Variáveis Globais
    [Header("Configurações:")] 
    [SerializeField] private float disableInterval;
    [SerializeField] private ChangeDevice device;
    
    [Header("Player Input:")]
    [SerializeField] private bool playerColliding = false;
    [SerializeField] private bool keyPressed = false;

    [Header("Sprites")] 
    [SerializeField] private Sprite enableSprite;
    [SerializeField] private Sprite disableSprite;

    // Componentes:
    private SpriteRenderer _spr;

    // Referências:
    private static CollisionLayersManager _collisionLayersManager;

    private bool _canDisable = true;
    #endregion

    #region Funções Unity

    private void Awake() => _collisionLayersManager = GameObject.FindObjectOfType<CollisionLayersManager>();

    private void Start() => _spr = GetComponent<SpriteRenderer>();
    
    private void Update()
    {
        // Input
        if (Input.GetKeyDown(KeyCode.Space))
            keyPressed = true;
        else
            keyPressed = false;

        // Desabilitando
        if (_canDisable && keyPressed && playerColliding)
        {
            _canDisable = false;
            DisableDevice();
            StartCoroutine(SetDisableInterval(disableInterval));
        }
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

    #region Funções Próprias
    private void DisableDevice()
    {
        _spr.sprite = disableSprite;
        device.Disable();

        var nearGuard = GuardBehaviour.Guards[0];
        foreach (var g in GuardBehaviour.Guards)
        {
            if (Vector3.Distance(g.transform.position, transform.position) <
                Vector3.Distance(nearGuard.transform.position, transform.position))
                nearGuard = g;
        }

        nearGuard.CheckPosition = transform.position;
        nearGuard.SetState(GuardBehaviour.GuardStates.Check);
    }

    private IEnumerator SetDisableInterval(float t)
    {
        yield return new WaitForSeconds(t);
        EnableDevice();
    }

    private void EnableDevice()
    {
        device.Enable();
        _canDisable = true;
        _spr.sprite = enableSprite;
    }
    #endregion
}
