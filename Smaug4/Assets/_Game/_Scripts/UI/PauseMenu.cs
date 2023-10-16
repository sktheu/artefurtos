using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region Global Variables

    public static bool gamePaused = false;

    public GameObject canvasMenu;

    public GameObject pauseMenu;

    public GameObject optionsMenuUI;

    public GameObject staminaCanvas;

    private LevelManager _levelManager;

    //Se for 1 está mirando, se for 0 não está
    [SerializeField]
    private int verifyAim = 0;

    public static string menuName;

    #endregion

    #region Unity Functions

    private void Awake()
    {
        _levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        staminaCanvas = GameObject.Find("StaminaBar_CanvasGroup");
    }

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

    #endregion

    #region Personal Functions

    public void Resume()
    {
        canvasMenu.SetActive(false);
        optionsMenuUI.SetActive(false);
        staminaCanvas.SetActive(true);
        Time.timeScale = 1f;
        gamePaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        //Verificar se vai ativar ou não a mira
        /*if (verifyAim == 1)
        {
            _ShootBall.IsAiming = true;
            verifyAim = 0;
        }
        else
            verifyAim = 0;*/
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
        //Sumir com a mira e parar animação
        /*if (_ShootBall.IsAiming == true)
        {
            verifyAim = 1;
            _ShootBall.IsAiming = false;
        }
        else
            verifyAim = 0;*/
            
    }

    public void LoadMenu()
    {
        //Time.timeScale = 1f;
        //SceneManager.LoadScene("Menu 1");
        Debug.Log("Foi pro menu principal");
    }

    public void Restart()
    {
        //int cenaAtual = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(cenaAtual);
        Time.timeScale = 1;
        _levelManager.Restart();
    }

    #endregion
}