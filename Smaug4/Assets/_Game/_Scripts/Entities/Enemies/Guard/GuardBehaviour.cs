using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardBehaviour : MonoBehaviour
{
    #region Variáveis Globais
    // Unity Inspector:
    [Header("Velocidades de cada estado:")]
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float checkSpeed;
    [SerializeField] private float alertSpeed;

    [Header("Máquina de Estados:")]
    [SerializeField] private InitialGuardState initialState;
    [SerializeField] private string curStateName;

    // Componentes:
    private NavMeshAgent _agent;
    private SpriteRenderer _spr;

    // StateMachine Variáveis:
    private GuardStateMachine _guardStateMachine;

    private enum InitialGuardState
    {
        Patrol,
        Chase,
        Check,
        Alert
    }
    #endregion

    #region Funções Unity
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _guardStateMachine = new GuardStateMachine(_agent);

        SetInitialState();
    }

    private void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _guardStateMachine.ExecuteState();

        // Mostre no inspector o nome do estado atual
        curStateName = _guardStateMachine.GetCurrentState().GetName();
    }
    #endregion

    #region Funções Próprias
    private void SetInitialState()
    {
        switch (initialState)
        {
            case InitialGuardState.Patrol:
                _guardStateMachine.ChangeState(new GuardPatrolState(), patrolSpeed);
                break;
            case InitialGuardState.Chase:
                _guardStateMachine.ChangeState(new GuardChaseState(), chaseSpeed);
                break;
            case InitialGuardState.Check:
                _guardStateMachine.ChangeState(new GuardCheckState(), checkSpeed);
                break;
            case InitialGuardState.Alert:
                _guardStateMachine.ChangeState(new GuardAlertState(), alertSpeed);
                break;
        }
    }
    #endregion
}
