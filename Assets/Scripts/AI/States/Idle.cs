using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State, IProtocol
{
    [Header("Wait Time Range")]
    [SerializeField] private float minWaitTime = 1;
    [SerializeField] private float maxWaitTime = 5;

    private AIFieldOfView fov;

    private void Start()
    {
        fov = this.GetComponent<AIFieldOfView>();
    }

    private void Update()
    {
        if (agentStateMachine.currState == EStates.IDLE)
        {
            if (fov.isPlayerVisible())
            {
                agentStateMachine.ChangeState(EStates.GREET);
            }
        }
    }

    public void Enter()
    {
        movementHandler.StopMoving();

        animationHandler.SetTrigger(animationParameterName);

        StartCoroutine(ILookAround());
    }

    public void Exit()
    {
        StopAllCoroutines();
    }

    private IEnumerator ILookAround()
    {
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);

        agentStateMachine.ChangeState(EStates.PATROL);
    }
}
