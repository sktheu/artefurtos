using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Vari�veis Globais
    // Inspector:
    [Header("Transi��o:")]
    [SerializeField] private TransitionSettings transitionSettings;
    [SerializeField] private float loadTime;
    #endregion

    #region Fun��es Pr�prias
    public void End()
    {
        // Ir para o menu de sele��o de fases
        // Desbloquear fase nova
        TransitionManager.Instance().Transition(SceneManager.GetActiveScene().name, transitionSettings, loadTime);
    }

    public void Restart() => TransitionManager.Instance().Transition(SceneManager.GetActiveScene().name, transitionSettings, loadTime);
    #endregion
}
