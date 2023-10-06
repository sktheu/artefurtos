using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    #region Variáveis Globais:
    [Header("Configurações:")]
    public bool CanFollow = true;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followSpeed;

    [Header("Min/Max Positions:")]
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;

    // Referências
    private Transform _playerTransf;
    #endregion

    #region Funções Unity
    private void Start() => _playerTransf = GameObject.FindGameObjectWithTag("Player").transform;

    private void LateUpdate()
    {
        if (CanFollow)
        {
            float xClamp = Mathf.Clamp(_playerTransf.position.x, xMin, xMax);
            float yClamp = Mathf.Clamp(_playerTransf.position.y, yMin, yMax);

            Vector3 targetpos = _playerTransf.transform.position + offset;
            Vector3 clampedpos = new Vector3(Mathf.Clamp(targetpos.x, xMin, xMax), Mathf.Clamp(targetpos.y, yMin, yMax), 0);

            SetNewPosition(targetpos);
        }
    }
    #endregion

    #region Funções Próprias
    public void SetNewPosition(Vector2 pos)
    {
        Vector3 newPos = (Vector3)pos + new Vector3(0, 0, -10f);
        transform.position = Vector3.Lerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
    #endregion
}