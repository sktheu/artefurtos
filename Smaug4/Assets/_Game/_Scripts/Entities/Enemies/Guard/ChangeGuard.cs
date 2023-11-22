using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGuard : MonoBehaviour
{
    #region Variáveis Globais
    // Unity Inspector:
    [Header("Configurações:")]
    [SerializeField] private float maxValue;
    [SerializeField] private float increment;
    [SerializeField] private float decrement;

    // Modificador do Progresso:
    public enum ProgressModifier { Decrease, Increase }

    // Componentes:
    private GuardBehaviour _guardBehaviour;

    // Progresso:
    private float _currentValue = 0f;
    #endregion

    #region Funções Unity
    private void Start() => _guardBehaviour = GetComponent<GuardBehaviour>();
    #endregion

    #region Funções Próprias
    public void ChangeProgress(ProgressModifier modifier)
    {
        if (modifier == ProgressModifier.Increase)
        {
            _currentValue = Mathf.Clamp(_currentValue + increment * Time.deltaTime, 0f, maxValue);
            VerifyIncrease();
        }
        else
        {
            //_currentValue = Mathf.Clamp(_currentValue - decrement * Time.deltaTime, 0f, maxValue);
            //VerifyDecrease();
        }
    }

    private void VerifyIncrease()
    {
        if (_currentValue == maxValue)
            _guardBehaviour.SetState(GuardBehaviour.GuardStates.Chase);
    }

    private void VerifyDecrease()
    {
        if (_currentValue == 0f)
        {
            if (_guardBehaviour.GuardStateMachine.CompareState(typeof(GuardChaseState)))
                _guardBehaviour.SetState(GuardBehaviour.GuardStates.Alert);
            else if (_guardBehaviour.GuardStateMachine.CompareState(typeof(GuardAlertState)))
                _guardBehaviour.SetState(GuardBehaviour.GuardStates.Patrol);
        }
    }
    #endregion
}
