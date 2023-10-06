using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GuardStateMachine
{
    #region Variáveis Globais
    public NavMeshAgent Agent { get; set; }
    private GuardState _currentState;

    public GuardStateMachine(NavMeshAgent agent) => this.Agent = agent;
    #endregion

    #region Funções Próprias
    public void ChangeState(GuardState newState, float moveSpeed, float acceleration)
    {
        _currentState = newState;
        _currentState.StateMachine = this;
        _currentState.MoveSpeed = moveSpeed;
        _currentState.Acceleration = acceleration;
    }

    public bool CompareState(Type targetState)
    {
        if (_currentState.GetType() == targetState)
            return true;

        return false;
    }

    public GuardState GetCurrentState()
    {
        return _currentState;
    }

    public void ExecuteState() => _currentState.Execute();
    #endregion
}