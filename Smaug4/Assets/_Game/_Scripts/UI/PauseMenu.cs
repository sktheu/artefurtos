using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region Global Variables
    // Inspector:
    [Header("Transição:")]
    [SerializeField] private TransitionSettings[] transitionSettings = new TransitionSettings[2];
    [SerializeField] private float transitionDelay;

    [Header("Cursor:")]
    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Texture2D clickCursor;

    // Referências:
    private AudioManager _audioManager;

    public static bool gamePaused = false;

    public GameObject canvasMenu;

    public GameObject pauseMenu;

    public GameObject optionsMenuUI;

    public GameObject staminaCanvas;

    private LevelManager _levelManager;

    public static string menuName;

    #endregion

    #region Unity Functions

    private void Awake()
    {
        _levelManager = GameObject.FindObjectOfType<LevelManager>();
        //_audioManager = GameObject.FindObjectOfType<AudioManager>();
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

        if (gamePaused)
        {
            if (Input.GetMouseButtonDown(0))
                Cursor.SetCursor(clickCursor, Vector2.zero, CursorMode.ForceSoftware);

            if (Input.GetMouseButtonUp(0))
                Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
    #endregion

    #region Personal Functions
    public void Resume()
    {
        canvasMenu.SetActive(false);
        optionsMenuUI.SetActive(false);
        if (staminaCanvas != null) staminaCanvas.SetActive(true);
        Time.timeScale = 1f;
        gamePaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        //_audioManager.PlaySFX("despause");
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
        if (staminaCanvas != null) staminaCanvas.SetActive(false);
        Time.timeScale = 0f;
        gamePaused = true;
        Cursor.visible = true;
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.None;
        //_audioManager.PlaySFX("pause");
        //Sumir com a mira e parar animação
        /*if (_ShootBall.IsAiming == true)
        {
            verifyAim = 1;
            _ShootBall.IsAiming = false;
        }
        else
            verifyAim = 0;*/
    }

    public void LoadMenu() => Invoke("LoadSelectionMenu", 0.28f);
    
    private void LoadSelectionMenu()
    {
        SceneManager.LoadScene("SelectionMenu");
        Time.timeScale = 1f;
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