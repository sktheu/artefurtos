using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSfx : MonoBehaviour
{
    #region Vari�veis Globais
    // Refer�ncias:
    private AudioManager _audioManager;

    private static int _curSfxIndex = 1;
    #endregion

    #region Fun��es Unity
    private void Awake() => _audioManager = GameObject.FindObjectOfType<AudioManager>();
    #endregion

    #region Fun��es Pr�prias
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
