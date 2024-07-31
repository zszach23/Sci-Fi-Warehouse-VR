using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent))]
public class AIMovement : MonoBehaviour
{
    public CharacterStats characterStats;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Rigidbody rigidBody;

    private void Awake()
    {
        Init();
    }

    //-----------------//
    private void Init()
    //-----------------//
    {
        rigidBody = this.GetComponent<Rigidbody>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// Rotate and Move towards a target game object
    /// </summary>
    /// <param name="target"></param>
    //-----------------------------------------//
    public void MoveToTarget(Transform target)
    //-----------------------------------------//
    {
        RotateToTarget(target.position);
        MoveToDestination(target.position);

    } // END MoveToTarget()

    /// <summary>
    /// Move with Rigidbody based on NavMeshAgent. Target is our destination.
    /// </summary>
    /// <param name="target"></param>
    //--------------------------------------------------------//
    public void MoveToDestination(Vector3 destinationPosition)
    //--------------------------------------------------------//
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(destinationPosition);
        rigidBody.velocity = navMeshAgent.velocity.normalized * characterStats.moveSpeed;

    } // END MoveToDestination()

    //----------------------//
    public void StopMoving()
    //----------------------//
    {
        navMeshAgent.isStopped = true;
        rigidBody.velocity = Vector3.zero;
        navMeshAgent.velocity = Vector3.zero;

    } // END StopMoving()

    /// <summary>
    /// Rotate to look at a target position
    /// </summary>
    /// <param name="target"></param>
    //------------------------------------------------//
    public void RotateToTarget(Vector3 targetPosition)
    //------------------------------------------------//
    {
        Debug.DrawLine(this.transform.position, targetPosition, Color.green);
        Quaternion targetRotation = GetTargetRotation(targetPosition);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetRotation, characterStats.rotationSpeed * Time.deltaTime);

    } // END RotateToTarget()

    /// <summary>
    /// Get a quaternion that represents the rotation of self facing targetPosition
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    //---------------------------------------------------------//
    public Quaternion GetTargetRotation(Vector3 targetPosition)
    //---------------------------------------------------------//
    {
        Vector3 direction = targetPosition - this.transform.position;
        direction.y = 0;
        direction.Normalize();

        if (direction == Vector3.zero)
        {
            direction = this.transform.forward;
        }

        return Quaternion.LookRotation(direction);

    } // END GetTargetRotation()
}
