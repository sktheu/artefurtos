using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CallGuards : MonoBehaviour
{
    #region Variáveis Globais
    // Unity Inspector:
    [Header("Configurações:")]
    [SerializeField] private bool isCamera = false;
    [SerializeField] private float maxAlertProgress;
    [SerializeField] private float alertIncrement;
    [SerializeField] private float alertDecrement;

    // Referências:
    private static AudioManager _audioManager;

    // Modificador do Progresso de Alerta:
    public enum AlertModifier { Decrease, Increase, Maximize }

    // Alert:
    private float _currentAlertProgress;

    // SFX:
    private bool _canPlaySfx = true;
    #endregion

    #region Funções Unity
    //private void Awake() => _audioManager = GameObject.FindObjectOfType<AudioManager>();

    //private void Update() => print(_currentAlertProgress);
    #endregion

    #region Funções Próprias
    public void ChangeAlertProgress(AlertModifier modifier)
    {
        if (modifier == AlertModifier.Increase)
        {
            _currentAlertProgress = Mathf.Clamp(_currentAlertProgress + alertIncrement * Time.deltaTime, 0f, maxAlertProgress);

            /*
            if (_canPlaySfx)
            {
                _canPlaySfx = false;
                _audioManager.PlaySFX("chamando_guardas");
                StartCoroutine(SetSfxInterval(3f));
            }
            */

            if (_currentAlertProgress >= maxAlertProgress) // Player totalmente avistado
            {
                // Chamar os Guardas
                ChangeGuards();

                // Caso for uma Camera
                if (isCamera)
                    GetComponent<CameraRotation>().Stop();
            }
        }
        else if (modifier == AlertModifier.Decrease)
        {
            _currentAlertProgress = Mathf.Clamp(_currentAlertProgress - alertDecrement * Time.deltaTime, 0f, maxAlertProgress);

            //Caso for uma Camera
            if (isCamera)
                GetComponent<CameraRotation>().Restart();
        }
        else
        {
            _currentAlertProgress = maxAlertProgress;
            // Chamar os Guardas
            ChangeGuards();
        }
    }

    public void ResetAlertProgress() => _currentAlertProgress = 0f;
    
    private void ChangeGuards()
    {
        foreach (var g in GuardBehaviour.Guards) 
            g.SetState(GuardBehaviour.GuardStates.Chase);
    }

    private IEnumerator SetSfxInterval(float interval)
    {
        yield return new WaitForSeconds(interval);
        _canPlaySfx = true;
    }
    #endregion
}
