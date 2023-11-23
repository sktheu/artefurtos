using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountCanvas : MonoBehaviour
{
    [HideInInspector] public ShootBall _shootBall;
    public TextMeshProUGUI text;
    public int contador;

    void Start()
    {
        //componente
        _shootBall = FindObjectOfType<ShootBall>();
    }

    void Update()
    {
        contador = _shootBall._ballCurCount;
        text.text = "x " + contador.ToString();
    }

}
