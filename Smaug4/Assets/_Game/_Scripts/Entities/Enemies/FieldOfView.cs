using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    #region Variáveis Globais
    // Unity Inspector:
    [Header("Configurações:")] 
    [SerializeField] private bool isCamera = false;
    [SerializeField] private float fovAngle = 90f;
    [SerializeField] private float fovRange = 8f;
    [SerializeField] private LayerMask ignoreLayers;

    // Referências:
    private Transform _fovPoint;
    private static Transform _target;
    private CollisionLayersManager _collisionLayersManager;

    // Componentes:
    private CallGuards _cameraAlertScript;
    #endregion

    #region Funções Unity
    private void Awake() => _collisionLayersManager = GameObject.FindObjectOfType<CollisionLayersManager>();

    private void Start()
    {
        if (isCamera)
            _cameraAlertScript = GetComponent<CallGuards>();
        else
            _cameraAlertScript = null;

        _fovPoint = transform.Find("FOV Point").transform;
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() => Detecting();
    #endregion

    #region Funções Próprias
    private void Detecting()
    {
        var dir = _target.position - transform.position;
        var angle = Vector3.Angle(dir, _fovPoint.up);
        var rayHit = Physics2D.Raycast(_fovPoint.position, dir, fovRange, ~ignoreLayers);

        if (angle < fovAngle / 2)
        {
            if (rayHit)
            {
                // Player Avistado
                if (rayHit.collider.gameObject.layer == _collisionLayersManager.Player.Index)
                {
                    if (isCamera)
                        _cameraAlertScript.ChangeAlertProgress(CallGuards.AlertModifier.Increase);
                    
                    Debug.DrawRay(_fovPoint.position, dir, Color.green);
                }
                else
                {
                    if (isCamera)
                        _cameraAlertScript.ChangeAlertProgress(CallGuards.AlertModifier.Decrease);
                }
            }
            else
            {
                if (isCamera)
                    _cameraAlertScript.ChangeAlertProgress(CallGuards.AlertModifier.Decrease);
            }
        }
    }
    #endregion
}
