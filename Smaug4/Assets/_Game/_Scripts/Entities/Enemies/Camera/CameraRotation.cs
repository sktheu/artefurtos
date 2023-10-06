using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    #region Variáveis Globais
    // Unity Inspector:
    [Header("Configurações:")]
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float pauseInterval;

    // Rotação
    private float _currentDir = 1f;
    private bool _canRotate = true;
    #endregion

    #region Funções Unity
    private void Update()
    {
        if (_canRotate)
        {
            VerifyAngle();
            ApplyRotation();
        }
    }
    #endregion

    #region Funções Próprias
    private void VerifyAngle()
    {
        var angleZ = transform.eulerAngles.z;
        if (angleZ >= 315f && angleZ <= 330f || angleZ >= 225f && angleZ <= 240f || angleZ >= 135f && angleZ <= 150f || angleZ >= 45f && angleZ <= 60f)
        {
            _currentDir *= -1f;
            _canRotate = false;
            StartCoroutine(PauseRotation(pauseInterval));
        }
    }

    private void ApplyRotation() => transform.Rotate(Vector3.forward, _currentDir * rotateSpeed * Time.deltaTime);

    private IEnumerator PauseRotation(float t)
    {
        yield return new WaitForSeconds(t);
        _canRotate = true;
    }

    public void Stop()
    {
        StopAllCoroutines();
        _canRotate = false;
    }

    public void Restart()
    {
        StopAllCoroutines();
        _canRotate = true;
    } 
    #endregion
}
