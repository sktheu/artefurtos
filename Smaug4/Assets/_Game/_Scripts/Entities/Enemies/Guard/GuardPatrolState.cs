using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class GuardPatrolState : GuardState
{
    #region Váriaveis Globais
    private Transform[] _patrolPoints;
    private int _patrolIndex = 0;
    private bool _isReturning = false;
    #endregion

    #region Funções Próprias
    public GuardPatrolState(Transform[] patrolPoints)
    {
        this._patrolPoints = patrolPoints;
    }

    public override void Execute()
    {
        base.Execute();
        base.StateMachine.Agent.SetDestination(_patrolPoints[_patrolIndex].position);

        if (base.HasReachPoint())
            ChangePatrolPoint();
    }

    private void ChangePatrolPoint()
    {
        if (_patrolIndex == _patrolPoints.Length - 1)
            _isReturning = true;
        else if (_patrolIndex == 0)
        {
            _isReturning = false;
        }

        if (!_isReturning)
            _patrolIndex++; // Fazendo o caminho indo
        else
            _patrolIndex--; // Fazendo o caminho voltando
    }
    #endregion
}
