using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    #region Global variables

    [Header("Stamina Parameters")]
    public float playerStamina = 100f;
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] public bool hasStamina= true;
    [SerializeField] public bool isSprinting = false;

    [Header("Stamina Regen")]
    [SerializeField] private float drain = 0.5f;
    [SerializeField] private float regen = 0.5f;

    [Header("Stamina UI")]
    [SerializeField] private Image staminaProgressUI = null;
    [SerializeField] private CanvasGroup sliderCanvasGroup = null;

    [HideInInspector] public PlayerMagroMov _playerMagroMov;
    private ShootBall _shootBall;

    private PlayerGameOver _playerGameOver;
    #endregion

    #region Unity Functions

    private void Start()
    {
        _playerMagroMov = GetComponent<PlayerMagroMov>();
        _shootBall = GetComponent<ShootBall>();
        _playerGameOver = GetComponent<PlayerGameOver>();
    }

    private void Update()
    {
        //Caso não esteja correndo
        if (!isSprinting)
        {
            if (playerStamina <= maxStamina - 0.01)
            {
                //Regenera barra de stamina
                playerStamina += regen * Time.deltaTime;
                UpdateStamina(1);

                //Não está mais correndo e os valores tão no máx
                if (playerStamina >= maxStamina)
                {
                    sliderCanvasGroup.alpha = 0;
                    hasStamina = true;
                }
            }
        }

        // Não poderá correr caso estiver atirando ou o jogo ter finalizado
        if (_shootBall.IsAiming || _playerGameOver.GameEnded) return;

        //Só pra ativar e desativar a variável isSprinting
        if (Input.GetKey(KeyCode.LeftShift) & 
           (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.1f))
        {
          Sprinting();
        }
        else
            isSprinting = false;

        if (playerStamina > 5)
            hasStamina = true;
        
    }

    #endregion

    #region Custom Functions
      
    //Código de quando o jogador estiver correndo
    public void Sprinting()
    {
        if (hasStamina)
        {
            if (playerStamina <= 0)
            {
                isSprinting = false;
                hasStamina = false;
                sliderCanvasGroup.alpha = 0;
            }
            else
            {
                isSprinting = true;
                playerStamina -= drain * Time.deltaTime;
                UpdateStamina(1);
            }
        }
    }

    //Some e aparece com a barra de stamina
    private void UpdateStamina(int value)
    {
        staminaProgressUI.fillAmount = playerStamina / maxStamina;

        if (value == 0)
            sliderCanvasGroup.alpha = 0;
        else
            sliderCanvasGroup.alpha = 1;
    }

    #endregion
}
