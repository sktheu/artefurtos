using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAlertState : GuardState
{
    #region Variáveis Globais
    private Vector3 _lastPlayerPos;
    #endregion

    #region Funções Próprias
    public GuardAlertState(Vector3 lastPlayerPos)
    {
        _lastPlayerPos = lastPlayerPos;
    }

    public override void Execute()
    {
        base.Execute();
        base.StateMachine.Agent.SetDestination(_lastPlayerPos);

        if (base.HasReachPoint())
        {
            // Chame a função que troque a direção em que o guarda está olhando
        }
    }
    #endregion
}
