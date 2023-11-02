using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BillLevelSelector : MonoBehaviour
{
    public Button[] buttons;

    private void Awake()
    {
        //Desbloqueia o nível 1
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevelBill", 1);

        //Bloqueia o botão do nível

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        //Desbloqueia o botão do nível
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void OpenLevel(int levelId)
    {
        string levelName = "testLevel " + levelId;
        SceneManager.LoadScene(levelName);
    }
}
