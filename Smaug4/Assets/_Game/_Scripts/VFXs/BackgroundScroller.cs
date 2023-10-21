using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    #region Variáveis Globais
    // Inspector:
    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;

    // Componentes:
    private RawImage _rawImage;

    private Vector3 _startPosition;
    #endregion

    #region Funções Unity
    private void Start() => _rawImage = GetComponent<RawImage>();

    private void Update() => _rawImage.uvRect = new Rect(_rawImage.uvRect.position + new Vector2(xSpeed, ySpeed) * Time.deltaTime, _rawImage.uvRect.size);
    #endregion
}
