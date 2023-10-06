using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ChangeDevice : MonoBehaviour
{
    [Header("Configurações:")] 
    [SerializeField] private Color disableColor;

    [Header("Camera:")] 
    [SerializeField] private Sprite cameraEnable;
    [SerializeField] private Sprite cameraDisable;

    // Componentes:
    private FieldOfView _fov;
    private CallGuards _callGuards;
    private Light2D[] _lights = new Light2D[2];
    
    // Movimento:
    private LaserBehaviour _laserBehaviour;
    private CameraRotation _cameraRotation;

    // Luz:
    private Color _defaultColor;

    // Camera:
    private SpriteRenderer _spr;

    private void Start()
    {
        _callGuards = GetComponent<CallGuards>();
        
        if (GetComponent<LaserBehaviour>() != null)
        {
            _laserBehaviour = GetComponent<LaserBehaviour>();
            _lights[0] = GetComponent<Light2D>();
        }
        else if (GetComponent<CameraRotation>() != null)
        {
            _fov = GetComponent<FieldOfView>();
            _cameraRotation = GetComponent<CameraRotation>();
            _lights[0] = transform.Find("Light 2D").GetComponent<Light2D>();
            _lights[1] = transform.Find("Extra Light").GetComponent<Light2D>();
            _spr = GetComponent<SpriteRenderer>();
        }

        _defaultColor = _lights[0].color;
    }

    public void Disable()
    {
        _callGuards.ResetAlertProgress();
        for (int i = 0; i < _lights.Length; i++)
        {
            if (_lights[i] != null)
                _lights[i].color = disableColor;
        }

        if (_laserBehaviour != null)
        {
            _laserBehaviour.Stop();
        }
        else if (_cameraRotation != null)
        {
            _fov.enabled = false;
            _cameraRotation.Stop();
            _spr.sprite = cameraDisable;
        }
    }

    public void Enable()
    {
        for (int i = 0; i < _lights.Length; i++)
        {
            if (_lights[i] != null)
                _lights[i].color = _defaultColor;
        }

        if (_cameraRotation != null)
        {
            _fov.enabled = true;
            _cameraRotation.Restart();
            _spr.sprite = cameraEnable;
        }
    }
}
