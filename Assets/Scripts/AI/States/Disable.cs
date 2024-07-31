using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : State, IProtocol
{
    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Interaction Menu")]
    [SerializeField] private GameObject interactionMenu;

    [Header("Animation")]
    [SerializeField] private AgentLookAt head;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float lookTime;

    private AIFieldOfView fov;

    private void Start()
    {
        fov = this.GetComponent<AIFieldOfView>();
    }

    private void Update()
    {
        if (agentStateMachine.currState == EStates.DISABLED)
        {
            if (fov.isTargetInRange(player.position))
            {
                interactionMenu.SetActive(true);
            }
            else
            {
                interactionMenu?.SetActive(false);
            }
        } 
    }

    public void Enter()
    {
        movementHandler.StopMoving();
        
        target.position = player.position;
        head.target = this.target;

        head.enabled = true;

        StartCoroutine(StartDisableAnimation());
    }

    private IEnumerator StartDisableAnimation()
    {
        Vector3 initPosition = target.position;

        float _elapsedTime = 0;

        while (_elapsedTime < lookTime)
        {
            target.position = Vector3.Lerp(initPosition, endPosition, _elapsedTime / lookTime);
            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.position = endPosition;
    }

    public void Exit()
    {
        head.target = player;
        head.enabled = false;
    }

    public void ToggleInteractionMenu()
    {
        if (agentStateMachine.currState == EStates.DISABLED)
        {
            agentStateMachine.ChangeState(EStates.GREET);
        }
        else
        {
            agentStateMachine.ChangeState(EStates.DISABLED);
        }
    }
}
