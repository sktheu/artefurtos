using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLastPosition : MonoBehaviour
{
    #region Variáveis Globais
    [SerializeField] private float registerTimer;
    public static Vector3 Position { get; private set; }
    #endregion

    #region Funções Unity

    private void Awake() => Position = transform.position;

    private void Start() => StartCoroutine(RegisterLastPosition(registerTimer));

    #endregion

    #region Funções Próprias

    private IEnumerator RegisterLastPosition(float interval)
    {
        yield return new WaitForSeconds(registerTimer);
        Position = transform.position;
    }

    #endregion
}
