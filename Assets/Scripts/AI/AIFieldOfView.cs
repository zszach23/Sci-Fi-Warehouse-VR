using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AIFieldOfView : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private GameObject player;

    [Header("Vision Cone")]
    [SerializeField] private float viewRadius;

    [Range(0, 360f)] 
    [SerializeField] private float viewAngle;
    [SerializeField] private float verticalViewLimit = .5f;

    [Header("LayerMasks")]
    public LayerMask obstructiveLayers;

    //---------------------------//
    public bool isPlayerVisible()
    //---------------------------//
    {
        return isTargetInRange(player.transform.position) && !IsTargetObstructed(player.transform.position);
    }

    public bool isFacingPlayer(float maxViewAngle)
    {
        Vector3 _playerXZ = player.transform.position;
        _playerXZ.y = 0;

        Vector3 directionToTarget = (_playerXZ - this.transform.position).normalized;
        float angle = Vector3.Angle(this.transform.forward, directionToTarget);
        //Debug.Log(angle + " //////// " + maxViewAngle);
        return angle <= maxViewAngle;
    }

    //-------------------------------------------------//
    public bool isTargetInRange(Vector3 targetPosition)
    //-------------------------------------------------//
    {
        float distance = Vector3.Distance(this.transform.position, targetPosition);

        return distance <= viewRadius;
    }

    // Determine if the target is within the vision cone
    //------------------------------------------------//
    public bool IsTargetInView(Vector3 targetPosition)
    //------------------------------------------------//
    {
        if (targetPosition.y > verticalViewLimit || targetPosition.y < -verticalViewLimit)
            return false;

        Vector3 directionToTarget = (targetPosition - this.transform.position).normalized;
        return Vector3.Angle(this.transform.forward, directionToTarget) < viewAngle / 2;

    } // END IsTargetInView()

    // Determine if there is an obstruction between self and the target
    //----------------------------------------------------//
    public bool IsTargetObstructed(Vector3 targetPosition)
    //----------------------------------------------------//
    {
        bool isObstructed = Physics.Linecast(this.transform.position, targetPosition, obstructiveLayers);
        return isObstructed;

    } // END IsTargetObstructed()
}
