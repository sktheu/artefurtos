using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BillLevelSelector : MonoBehaviour
{
    #region Vari�veis Globais
    // Inspector:
    [Header("Transi��o:")]
    [SerializeField] private TransitionSettings[] transitionSettings = new TransitionSettings[2];
    [SerializeField] private float transitionDelay;

    [Header("Refer�ncias:")]
    public Button[] buttons;
    #endregion

    #region Fun��es Unity
    private void Start()
    {
        //Ativando cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        int unlockedLevel;

        if (PlayerPrefs.GetInt("UnlockedLevelBill") == 0)
        {
            PlayerPrefs.SetInt("UnlockedLevelBill", 1);
            unlockedLevel = PlayerPrefs.GetInt("UnlockedLevelBill");
        }
        else
        {
            unlockedLevel = PlayerPrefs.GetInt("UnlockedLevelBill");
        }
        //Debug.Log(unlockedLevel);

        //Bloqueia o bot�o do n�vel
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        //Desbloqueia o bot�o do n�vel
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
            buttons[i].gameObject.GetComponent<ChangeIcon>().Change();
        }
    }
    #endregion

    #region Fun��es Pr�prias
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
    #endregion
}
