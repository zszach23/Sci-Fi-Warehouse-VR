using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Greet : State, IProtocol
{
    private AIFieldOfView fov;

    [SerializeField] private GameObject player;
    [SerializeField] private AgentLookAt agentLookAt;
    [Space]
    [SerializeField] private float facingPlayerLimit = 50;

    [Header("Interaction")]
    [SerializeField] private GameObject interactionMenu;

    private void Start()
    {
        fov = this.GetComponent<AIFieldOfView>();
    }

    private void Update()
    {
        if (agentStateMachine.currState == EStates.GREET)
        {

        }
    }

    public void Enter()
    {
        movementHandler.StopMoving();

        animationHandler.SetTrigger(animationParameterName);
        StartCoroutine(IGreet());

        interactionMenu.SetActive(true);
    }

    public void Exit()
    {
        agentLookAt.enabled = false;
        StopAllCoroutines();

        interactionMenu.SetActive(false);
    }

    private IEnumerator IGreet()
    {
        yield return new WaitUntil(LookingAtPlayer);
        agentLookAt.enabled = true;
        yield return new WaitUntil(IsPlayerNotInGreetZone);

        agentStateMachine.ChangeState(EStates.IDLE);
    }

    private bool IsPlayerNotInGreetZone()
    {
        if (!fov.isPlayerVisible())
        {
            return true;
        }

        return false;
    }

    private bool LookingAtPlayer()
    {
        if (fov.isFacingPlayer(facingPlayerLimit))
        {
            return true;
        }

        movementHandler.RotateToTarget(player.transform.position);

        return false;
    }
}
