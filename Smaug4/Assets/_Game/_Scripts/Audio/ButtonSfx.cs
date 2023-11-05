using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSfx : MonoBehaviour
{
    #region Variáveis Globais
    // Referências:
    private AudioManager _audioManager;

    private static int _curSfxIndex = 1;
    #endregion

    #region Funções Unity
    private void Awake() => _audioManager = GameObject.FindObjectOfType<AudioManager>();
    #endregion

    #region Funções Próprias
    public void PlaySfx()
    {
        _audioManager.PlaySFX("confirmar_" + _curSfxIndex);

        if (_curSfxIndex == 1)
            _curSfxIndex = 2;
        else
            _curSfxIndex = 1;
    }
    #endregion
}
