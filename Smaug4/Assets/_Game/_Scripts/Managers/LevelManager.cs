using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Variáveis Globais
    // Inspector:
    [Header("Transição:")]
    [SerializeField] private TransitionSettings transitionSettings;
    [SerializeField] private float loadTime;
    #endregion

    #region Funções Próprias
    public void End()
    {
        // Ir para o menu de seleção de fases
        // Desbloquear fase nova
        TransitionManager.Instance().Transition(SceneManager.GetActiveScene().name, transitionSettings, loadTime);
    }

    public void Restart() => TransitionManager.Instance().Transition(SceneManager.GetActiveScene().name, transitionSettings, loadTime);
    #endregion
}
