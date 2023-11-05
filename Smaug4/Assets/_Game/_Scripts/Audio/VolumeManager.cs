using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    #region Variáveis Globais
    // Inspector:
    [Header("Referências:")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;
    #endregion

    #region Funções Unity
    private void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
            LoadVolume();
        else
            SetMasterVolume();
    }
    #endregion

    #region Funções Próprias
    //volume "Master" e não máximo
    public void SetMasterVolume()
    {
        float volume = volumeSlider.value;
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");

        SetMasterVolume();
    }
    #endregion
}


