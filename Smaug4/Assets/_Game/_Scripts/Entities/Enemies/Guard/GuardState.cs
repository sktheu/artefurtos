using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GuardState
{
    public GuardStateMachine StateMachine { get; set; }
    public float MoveSpeed { get; set; }

    public abstract void Execute();

    public string GetName()
    {
        return this.GetType().ToString();
    }
}
