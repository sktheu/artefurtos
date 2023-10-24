using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Variáveis Globais
    // Inspector:
    [Header("Configurações:")]
    [SerializeField] private string musicMenu;

    [Header("Transição:")]
    [SerializeField] private TransitionSettings[] transitionSettings = new TransitionSettings[2];
    [SerializeField] private float transitionDelay;

    [Header("Cursor:")]
    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Texture2D clickCursor;
    
    // Referências:
    private AudioManager _audioManager;
    private LevelManager _levelManager;

    public static int CurTransitionIndex = 0;
    #endregion

    #region Funções Unity

    private void Awake()
    {
        _audioManager = GameObject.FindObjectOfType<AudioManager>();
        _levelManager = GameObject.FindObjectOfType<LevelManager>();
        //_audioManager.PlayMusic(musicMenu);

        Cursor.visible = true;
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        TransitionManager.Instance().Transition(transitionSettings[CurTransitionIndex], transitionDelay);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Cursor.SetCursor(clickCursor, Vector2.zero, CursorMode.ForceSoftware);

        if (Input.GetMouseButtonUp(0))
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    #endregion

    #region Funções Próprias
    public void StartGame()
    {
        //Outro jeito de fazer
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        PauseMenu.menuName = SceneManager.GetActiveScene().name;
        TransitionManager.Instance().Transition("LevelSelector", transitionSettings[CurTransitionIndex], transitionDelay);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenHyperLink(string url)
    {
        Application.OpenURL(url);
    }

    public void ApplyTransition()
    {
        TransitionManager.Instance().Transition(transitionSettings[CurTransitionIndex], transitionDelay);

        if (CurTransitionIndex == 0)
            CurTransitionIndex = 1;
        else
            CurTransitionIndex = 0;
    }
    #endregion
}
