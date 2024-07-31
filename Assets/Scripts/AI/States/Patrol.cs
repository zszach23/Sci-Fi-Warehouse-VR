using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Patrol : State, IProtocol
{
    [Header("Patrol Points")]
    [SerializeField] private List<Transform> patrolPoints;
    [SerializeField] private int currentIndex = 0;

    [Header("SFX")]
    public AudioClip[] FootstepAudioClips;
    [Range(0, 1)] public float footstepAudioVolume = 0.5f;

    private AIFieldOfView fov;

    private void Start()
    {
        fov = this.GetComponent<AIFieldOfView>();
    }

    private void Update()
    {
        if (agentStateMachine.currState == EStates.PATROL)
        {
            if (fov.isPlayerVisible())
            {
                agentStateMachine.ChangeState(EStates.GREET);
            }
        }
    }

    public void Enter()
    {
        animationHandler.SetTrigger(animationParameterName);
        StartCoroutine(GoToNextNode());
    }

    public void Exit()
    {
        StopAllCoroutines();
    }

    private IEnumerator GoToNextNode()
    {
        movementHandler.navMeshAgent.SetDestination(patrolPoints[currentIndex].position);
        yield return new WaitUntil(MoveToNextNode);
        currentIndex = (currentIndex + 1) % patrolPoints.Count;
        agentStateMachine.ChangeState(EStates.IDLE);
    }

    private bool MoveToNextNode()
    {
        if (!movementHandler.navMeshAgent.pathPending && movementHandler.navMeshAgent.remainingDistance < movementHandler.navMeshAgent.stoppingDistance)
            return true;

        movementHandler.MoveToTarget(patrolPoints[currentIndex]);

        return false;
    }

    public void OnFootstep(AnimationEvent animationEvent)
    {
        if (FootstepAudioClips.Length > 0)
        {
            SoundManager.instance.PlayRandomSound(FootstepAudioClips, this.transform, footstepAudioVolume);
        }
    }
}
