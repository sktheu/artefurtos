using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class FadeLevelSelection : MonoBehaviour
{
    #region Variáveis Globais
    // Inspector:
    [Header("Referências:")]
    [SerializeField] private Animator iconAnim;

    [Header("Fade:")]
    [SerializeField] private float fadeSpeed;

    private bool _isOver = false;

    // Componentes:
    private Image _img;
    #endregion

    #region Funções Unity
    private void Start() => _img = GetComponent<Image>();

    private void Update()
    {
        ApplyFade(_isOver);
        iconAnim.SetBool("mouseColliding", _isOver);
        VerifyMouse();
    }
    #endregion

    #region Funções Próprias
    private void VerifyMouse()
    {
        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero, Mathf.Infinity);

        if (hit)
        {
            if (hit.collider.gameObject.name == gameObject.name)
                _isOver = true;
            else
                _isOver = false;
        }
    }

    private void ApplyFade(bool hide)
    {
        Color color = _img.color;
        var alpha = color.a;

        if (hide)
            alpha -= fadeSpeed * Time.deltaTime;
        else
            alpha += fadeSpeed * Time.deltaTime;

        color.a = alpha;
        _img.color = color;
    }
    #endregion
}
