using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GuardState
{
    #region Variáveis Globais
    public GuardStateMachine StateMachine { get; set; }
    public float MoveSpeed { get; set; }
    public float Acceleration { get; set; }

    #endregion

    #region Funções Próprias
    public virtual void Execute()
    {
        StateMachine.Agent.speed = MoveSpeed * Time.deltaTime;
        StateMachine.Agent.acceleration = Acceleration * Time.deltaTime;
    }

    public string GetName()
    {
        return this.GetType().ToString();
    }

    public bool HasReachPoint()
    {
        if (!StateMachine.Agent.pathPending)
        {
            if (StateMachine.Agent.remainingDistance <= StateMachine.Agent.stoppingDistance)
            {
                if (!StateMachine.Agent.hasPath || StateMachine.Agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }
    #endregion
}
