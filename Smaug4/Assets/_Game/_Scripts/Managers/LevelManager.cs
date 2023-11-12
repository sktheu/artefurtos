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
    [SerializeField] private TransitionSettings[] firstTransitionSettings = new TransitionSettings[2];
    [SerializeField] private float firstTransitionDelay;

    [Header("Configurações:")] 
    [SerializeField] private bool isGame = false;

    public static bool FirstTime = true;

    private enum CharacterType
    {
        Bill,
        Hasmula
    }
    #endregion

    #region Funções Unity
    private void Start()
    {
        if (isGame && FirstTime)
        {
            TransitionManager.Instance().Transition(firstTransitionSettings[MainMenu.CurTransitionIndex],
                firstTransitionDelay);
            FirstTime = false;
        }
    }
    #endregion

    #region Funções Próprias
    public void End()
    {
        if (characterType == CharacterType.Hasmula)
        {
            if (PlayerPrefs.GetInt("UnlockedLevelHasmula") < nextLevel)
                PlayerPrefs.SetInt("UnlockedLevelHasmula", nextLevel);
        }
        else
        {
            if (PlayerPrefs.GetInt("UnlockedLevelBill") < nextLevel)
                PlayerPrefs.SetInt("UnlockedLevelBill", nextLevel);
        }
        // Ir para o menu de seleção de fases
        // Desbloquear fase nova
        TransitionManager.Instance().Transition("SelectionMenu", transitionSettings, loadTime);
    }

    public void Restart() => TransitionManager.Instance().Transition(SceneManager.GetActiveScene().name, transitionSettings, loadTime);
    #endregion
}
