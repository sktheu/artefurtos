using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class GuardBehaviour : MonoBehaviour
{
    #region Variáveis Globais
    // Unity Inspector:
    [Header("Velocidades de cada estado:")]
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float checkSpeed;
    [SerializeField] private float alertSpeed;

    [Header("Aceleração para atingir a velocidade:")] 
    [SerializeField] private float acceleration;

    [Header("Máquina de Estados:")]
    [SerializeField] private GuardStates initialState;
    [SerializeField] private string curStateName;

    [Header("Pontos de Patrulhas:")] 
    [SerializeField] private Transform[] patrolPoints;

    [Header("Lanterna:")] 
    [SerializeField] private GameObject[] lanterns = new GameObject[4];

    [Header("Taser:")]
    [SerializeField] private GameObject[] tasers = new GameObject[4];

    // Componentes:
    private NavMeshAgent _agent;
    private Animator _animator;

    // StateMachine Variáveis:
    public GuardStateMachine GuardStateMachine;

    // Segurar Item:
    private string _lastName;

    // Check:
    [HideInInspector] public Vector3 CheckPosition;

    // Guardas:
    public static List<GuardBehaviour> Guards = new List<GuardBehaviour>();

    public enum GuardStates
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
        GuardStateMachine = new GuardStateMachine(_agent);
        
        // Desabilite as rotações feitas pelo NavMeshAgent
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        SetState(initialState);

        if (Guards.Count == 0)
            Guards = GameObject.FindObjectsOfType<GuardBehaviour>().ToList();
    }

    private void Start() => _animator = GetComponent<Animator>();

    private void Update() => Animate();       
    
    private void FixedUpdate()
    {
        GuardStateMachine.ExecuteState();

        // Mostre no inspector o nome do estado atual
        curStateName = GuardStateMachine.GetCurrentState().GetName();
    }
    #endregion

    #region Funções Próprias
    public void SetState(GuardStates state)
    {
        switch (state)
        {
            case GuardStates.Patrol:
                StopAllCoroutines();
                GuardStateMachine.ChangeState(new GuardPatrolState(patrolPoints), patrolSpeed, acceleration);
                break;
            case GuardStates.Chase:
                StopAllCoroutines();
                GuardStateMachine.ChangeState(new GuardChaseState(GameObject.FindGameObjectWithTag("Player").transform), chaseSpeed, acceleration);
                break;
            case GuardStates.Check:
                StopAllCoroutines();
                GuardStateMachine.ChangeState(new GuardCheckState(CheckPosition, this), checkSpeed, acceleration);
                break;
            case GuardStates.Alert:
                StopAllCoroutines();
                GuardStateMachine.ChangeState(new GuardAlertState(PlayerLastPosition.Position), alertSpeed, acceleration);
                break;
        }
    }

    private void Animate()
    {
        if (_agent.hasPath && _agent.velocity.magnitude > 0)
        {
            var moveDirection = (Vector2)_agent.velocity.normalized;

            _animator.SetFloat("moveX", moveDirection.x);
            _animator.SetFloat("moveY", moveDirection.y);
        }
        else
        {
            _animator.SetFloat("moveX", 0f);
            _animator.SetFloat("moveY", 0f);
        }
    }

    private void SetHoldItem(string name)
    {
        if (_lastName != name)
        {
            if (GuardStateMachine.CompareState(typeof(GuardChaseState)))
            {
                foreach (var t in tasers)
                {
                    if (t.name == name)
                        t.SetActive(true);
                    else
                        t.SetActive(false);
                }
            }
            else
            {
                foreach (var l in lanterns)
                {
                    if (l.name == name)
                        l.SetActive(true);
                    else
                        l.SetActive(false);
                }
            }

            _lastName = name;
        }
    }

    public void ReachedCheckPos() => StartCoroutine(SetCheckPosInterval(Random.Range(5f, 10f)));

    private IEnumerator SetCheckPosInterval(float t)
    {
        yield return new WaitForSeconds(t);
        SetState(GuardStates.Patrol);
    }
    #endregion
}
