using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCheckState : GuardState
{
    #region Variáveis Globais
    private Vector3 _checkPos;
    private GuardBehaviour _guardBehaviour;
    #endregion

    #region Funções Próprias
    public GuardCheckState(Vector3 checkPos, GuardBehaviour guardBehaviour)
    {
        this._checkPos = checkPos;
        this._guardBehaviour = guardBehaviour;
    }

    public override void Execute()
    {
        base.Execute();
        base.StateMachine.Agent.SetDestination(_checkPos);

        if (HasReachPoint())
            _guardBehaviour.ReachedCheckPos();
    }
    #endregion
}
