using System;
using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    #region Variáveis Globais
    // Inspector:
    [Header("Transição:")]
    [SerializeField] private TransitionSettings[] transitionSettings = new TransitionSettings[2];
    [SerializeField] private float transitionDelay;

    [Header("Referências:")]
    public Button[] buttons;
    #endregion

    #region Funções Unity
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        LevelManager.FirstTime = true;
        TransitionManager.Instance().Transition(transitionSettings[MainMenu.CurTransitionIndex],
            transitionDelay);

        if (MainMenu.CurTransitionIndex == 0)
            MainMenu.CurTransitionIndex = 1;
        else
            MainMenu.CurTransitionIndex = 0;

        //Ativando cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        int unlockedLevel;

        if (PlayerPrefs.GetInt("UnlockedLevelHasmula") == 0)
        {
            PlayerPrefs.SetInt("UnlockedLevelHasmula", 1);
            unlockedLevel = PlayerPrefs.GetInt("UnlockedLevelHasmula");
        }
        else
        {
            unlockedLevel = PlayerPrefs.GetInt("UnlockedLevelHasmula");
        }
        //Debug.Log(unlockedLevel);

        //Bloqueia o botão do nível
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        //Desbloqueia o botão do nível
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
            buttons[i].gameObject.GetComponent<ChangeIcon>().Change();
        }
    }
    #endregion

    #region Funções Próprias
    public void OpenLevel(int levelId) => StartCoroutine(LoadOpenLevel(levelId, 0.28f));

    private IEnumerator LoadOpenLevel(int levelId, float t)
    {
        yield return new WaitForSeconds(t);
        string levelName = "testLevel " + levelId;
        TransitionManager.Instance().Transition(levelName, transitionSettings[MainMenu.CurTransitionIndex],
            transitionDelay);

        if (MainMenu.CurTransitionIndex == 0)
            MainMenu.CurTransitionIndex = 1;
        else
            MainMenu.CurTransitionIndex = 0;
    }

    public void Back() => Invoke("LoadMainMenu", 0.28f);

    private void LoadMainMenu() => SceneManager.LoadScene("MainMenu");

    #endregion
}
