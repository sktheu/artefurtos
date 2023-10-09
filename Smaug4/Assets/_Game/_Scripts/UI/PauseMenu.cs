using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;

    public GameObject canvasMenu;

    public GameObject pauseMenu;

    public GameObject optionsMenuUI;

    public GameObject staminaCanvas;

    public static string menuName;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        canvasMenu.SetActive(false);
        optionsMenuUI.SetActive(false);
        staminaCanvas.SetActive(true);
        Time.timeScale = 1f;
        gamePaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Pause()
    {
        canvasMenu.SetActive(true);
        pauseMenu.SetActive(true);
        staminaCanvas.SetActive(false);
        Time.timeScale = 0f;
        gamePaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadMenu()
    {
        //Time.timeScale = 1f;
        //SceneManager.LoadScene("Menu 1");
        Debug.Log("Foi pro menu principal");
    }

    public void Restart()
    {
        //TROCAR PRO SISTEMA DO DUCA DEPOIS
        int cenaAtual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(cenaAtual);
    }

}