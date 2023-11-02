using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] buttons;

    private void Awake()
    {
        //Desbloqueia o nível 1
        int unlockedLevel;

        if (PlayerPrefs.GetInt("UnlockedLevelHasmula") == 0)
            unlockedLevel = PlayerPrefs.GetInt("UnlockedLevelHasmula", 1);
        else
            unlockedLevel = PlayerPrefs.GetInt("UnlockedLevelHasmula");

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

    public void OpenLevel(int levelId)
    {
        string levelName = "testLevel " + levelId;
        SceneManager.LoadScene(levelName);
    }
}
