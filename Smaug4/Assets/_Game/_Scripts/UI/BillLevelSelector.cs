using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BillLevelSelector : MonoBehaviour
{
    public Button[] buttons;

    private void Awake()
    {
        //Desbloqueia o n�vel 1
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

    public void OpenLevel(int levelId)
    {
        string levelName = "testLevel " + levelId;
        SceneManager.LoadScene(levelName);
    }
}