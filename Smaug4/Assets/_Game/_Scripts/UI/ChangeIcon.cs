using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeIcon : MonoBehaviour
{
    #region Variáveis Globais
    // Inspector:
    [SerializeField] private Sprite unlockedIcon;

    // Componentes:
    private Image _img;
    #endregion

    #region Funções Unity
    private void Start() => _img = GetComponent<Image>();

    #endregion

    #region Funções Próprias
    public void Change()
    {
        _img.sprite = unlockedIcon;
        Debug.Log("mudou icone");
    }
    #endregion
}
