using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Variáveis Globais
    // Inspector:
    [Header("Tipo de Personagem:")] 
    [SerializeField] private int nextLevel;
    [SerializeField] private CharacterType characterType;

    [Header("Transição:")]
    [SerializeField] private TransitionSettings transitionSettings;
    [SerializeField] private float loadTime;
    
    private enum CharacterType
    {
        Bill,
        Hasmula
    }
    #endregion

    #region Funções Próprias
    public void End()
    {
        // Ir para o menu de seleção de fases
        // Desbloquear fase nova
        TransitionManager.Instance().Transition(SceneManager.GetActiveScene().name, transitionSettings, loadTime);
    }

    public void Restart() => TransitionManager.Instance().Transition(SceneManager.GetActiveScene().name, transitionSettings, loadTime);

    public void Complete()
    {
        if (characterType == CharacterType.Hasmula)
            PlayerPrefs.SetInt("UnlockedLevelHasmula", nextLevel);
        else
            PlayerPrefs.SetInt("UnlockedLevelBill", nextLevel);
    }
    #endregion
}
