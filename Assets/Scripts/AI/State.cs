using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AgentStateMachine))]
public abstract class State : MonoBehaviour
{
    [HideInInspector] protected AgentStateMachine agentStateMachine;
    [HideInInspector] protected Animator animationHandler;
    [HideInInspector] protected AIMovement movementHandler;

    [Header("Animation")]
    [SerializeField] protected string animationParameterName;

    protected virtual void Awake()
    {
        agentStateMachine = this.GetComponent<AgentStateMachine>();
        animationHandler = this.GetComponent<Animator>();
        movementHandler = this.GetComponent<AIMovement>();
    }

}
