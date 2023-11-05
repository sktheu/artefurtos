using System.Collections;
using System.Collections.Generic;
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
    private Animator _anim;
    #endregion

    #region Funções Unity
    private void Start()
    {
        _img = GetComponent<Image>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        _anim.SetBool("mouseColliding", _isOver);
        iconAnim.SetBool("mouseColliding", _isOver);
        VerifyMouse();
    }
    #endregion

    #region Funções Próprias
    private void VerifyMouse()
    {
        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero, 1f);

        if (hit)
        {
            if (hit.collider.gameObject.name == gameObject.name)
                _isOver = true;
            else
                _isOver = false;
        }
    }
    #endregion
}
