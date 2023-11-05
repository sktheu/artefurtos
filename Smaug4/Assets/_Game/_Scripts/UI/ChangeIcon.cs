using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeIcon : MonoBehaviour
{
    #region Vari�veis Globais
    // Inspector:
    [SerializeField] private Sprite unlockedIcon;

    // Componentes:
    private Image _img;
    #endregion

    #region Fun��es Unity
    private void Start() => _img = GetComponent<Image>();

    #endregion

    #region Fun��es Pr�prias
    public void Change()
    {
        _img.sprite = unlockedIcon;
        Debug.Log("mudou icone");
    }
    #endregion
}
