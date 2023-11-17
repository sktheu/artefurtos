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
    [SerializeField] private GameObject lanternParent;
    [SerializeField] private GameObject[] lanterns = new GameObject[4];

    [Header("Taser:")] 
    [SerializeField] private GameObject taserParent;
    [SerializeField] private GameObject[] tasers = new GameObject[4];

    [Header("FOV:")] 
    [SerializeField] private Transform fovParentTransform;

    [Header("Interagir Estação de Hacking:")] 
    [SerializeField] private Sprite[] hackingPlaceSprites;

    // Componentes:
    private NavMeshAgent _agent;
    private Animator _animator;
    private SpriteRenderer _spr;

    // StateMachine Variáveis:
    public GuardStateMachine GuardStateMachine;

    // Segurar Item:
    private string _lastName;

    // Check:
    [HideInInspector] public Vector3 CheckPosition;

    // Guardas:
    public static List<GuardBehaviour> Guards = new List<GuardBehaviour>();

    // Hacking Place:
    [HideInInspector] public bool IsInHackingPlace = false;

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

        Guards = GameObject.FindObjectsOfType<GuardBehaviour>().ToList();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!IsInHackingPlace)
            Animate();
        else
            AnimateInHackingPlace();

        ChangeFOV();
    }

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
                DefaultHoldItem(true);
                break;
            case GuardStates.Chase:
                StopAllCoroutines();
                GuardStateMachine.ChangeState(new GuardChaseState(GameObject.FindGameObjectWithTag("Player").transform), chaseSpeed, acceleration);
                DefaultHoldItem(false);
                break;
            case GuardStates.Check:
                StopAllCoroutines();
                GuardStateMachine.ChangeState(new GuardCheckState(CheckPosition, this), checkSpeed, acceleration);
                DefaultHoldItem(true);
                break;
            case GuardStates.Alert:
                StopAllCoroutines();
                GuardStateMachine.ChangeState(new GuardAlertState(PlayerLastPosition.Position, this), alertSpeed, acceleration);
                DefaultHoldItem(true);
                break;
        }
    }

    private void Animate()
    {
        if (_agent.hasPath && _agent.velocity.magnitude > 0)
        {
            if (_agent.velocity.magnitude > 0.65f)
            {
                var moveDirection = (Vector2)_agent.velocity.normalized;

                _animator.SetFloat("moveX", moveDirection.x);
                _animator.SetFloat("moveY", moveDirection.y);
            }
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
                lanternParent.SetActive(false);
                taserParent.SetActive(true);
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
                taserParent.SetActive(false);
                lanternParent.SetActive(true);
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

    private void DefaultHoldItem(bool isLantern)
    {
        GameObject[] items;
        if (isLantern)
        {
            lanternParent.SetActive(true);
            taserParent.SetActive(false);
            items = lanterns;
        }
        else
        {   
            taserParent.SetActive(true);
            lanternParent.SetActive(false);
            items = tasers;
        }

        var moveDirection = (Vector2)_agent.velocity.normalized;
        var desiredItem = "";

        if (moveDirection.x > 0)
            desiredItem = "Right";
        else if (moveDirection.x < 0)
            desiredItem = "Left";
        else if (moveDirection.y > 0)
            desiredItem = "Up";
        else
            desiredItem = "Down";

        foreach (var i in items)
        {
            if (i.name.Contains(desiredItem))
                i.SetActive(true);
            else
                i.SetActive(false);
        }
    }

    public void ReachedCheckPos() => StartCoroutine(SetCheckPosInterval(Random.Range(5f, 10f)));

    private IEnumerator SetCheckPosInterval(float t)
    {
        yield return new WaitForSeconds(t);
        SetState(GuardStates.Patrol);
    }

    private void ChangeFOV()
    {
        var moveDirection = (Vector2)_agent.velocity.normalized;

        if (moveDirection.x > 0)
            fovParentTransform.rotation = Quaternion.Euler(0f, 0f, 90f);
        else if (moveDirection.x < 0)
            fovParentTransform.rotation = Quaternion.Euler(0f, 0f, -90f);
        else if (moveDirection.y > 0)
            fovParentTransform.rotation = Quaternion.Euler(0f, 0f, 180f);
        else
            fovParentTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void AnimateInHackingPlace()
    {
        _animator.enabled = false;

        if (gameObject.name.Contains("Guard 2"))
            _spr.sprite = hackingPlaceSprites[2];
        else if (gameObject.name.Contains("Guard 1"))
            _spr.sprite = hackingPlaceSprites[1];
        else
            _spr.sprite = hackingPlaceSprites[0];

        lanternParent.gameObject.SetActive(false);
        fovParentTransform.gameObject.SetActive(false);
    }
    #endregion
}
