using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeIcon : MonoBehaviour
{
    [SerializeField] private Sprite unlockedIcon;

    // Componentes:
    private Image _img;

    private void Start() => _img = GetComponent<Image>();

    public void Change() => _img.sprite = unlockedIcon;
}
