using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAlertState : GuardState
{
    #region Variáveis Globais
    private Vector3 _lastPlayerPos;
    private GuardBehaviour _guardBehaviour;
    #endregion

    #region Funções Próprias
    public GuardAlertState(Vector3 lastPlayerPos, GuardBehaviour guardbehaviour)
    {
        this._lastPlayerPos = lastPlayerPos;
        this._guardBehaviour = guardbehaviour;
    }

    public override void Execute()
    {
        base.Execute();
        base.StateMachine.Agent.SetDestination(_lastPlayerPos);

        if (base.HasReachPoint())
            _guardBehaviour.ReachedCheckPos();
    }
    #endregion
}
