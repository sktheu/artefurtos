using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardFieldOfView : MonoBehaviour
{
    #region Vari�veis Globais
    // Unity Inspector:
    [Header("Refer�ncias:")]
    [SerializeField] private ChangeGuard changeGuard;

    private static CollisionLayersManager _collisionLayersManager;

    private bool _viewPlayer = false;
    #endregion

    #region Fun��es Unity
    private void Start() => _collisionLayersManager = FindObjectOfType<CollisionLayersManager>();

    private void Update()
    {
        if (_viewPlayer)
            changeGuard.ChangeProgress(ChangeGuard.ProgressModifier.Increase);
        else
            changeGuard.ChangeProgress(ChangeGuard.ProgressModifier.Decrease);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == _collisionLayersManager.Player.Index)
            _viewPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == _collisionLayersManager.Player.Index)
            _viewPlayer = false;
    }
    #endregion
}
