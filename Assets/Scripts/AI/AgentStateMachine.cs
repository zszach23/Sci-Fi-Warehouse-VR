using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentStateMachine : MonoBehaviour
{
    [Header("Agent State")]
    [SerializeField] private EStates startState;

    public EStates currState;
    private IProtocol _currentState;

    [Header("Cache States")]
    [SerializeField] private Idle idleState;
    [SerializeField] private Patrol patrolState;
    [SerializeField] private Greet greetState;
    [SerializeField] private Disable disableState;

    private void Start()
    {
        ChangeState(startState);
    }

    public void ChangeState(EStates newState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        switch (newState)
        {
            case EStates.IDLE:
                _currentState = idleState;
                break;

            case EStates.PATROL:
                _currentState = patrolState;
                break;

            case EStates.GREET:
                _currentState = greetState;
                break;

            case EStates.DISABLED:
                _currentState = disableState;
                break;
        }

        currState = newState;
        _currentState.Enter();
    }
}
