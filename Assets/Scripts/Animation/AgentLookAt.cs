using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

// Requires that the gameObject has an AimConstraint component.
[RequireComponent(typeof(AimConstraint))]
public class AgentLookAt : MonoBehaviour
{
    [Tooltip("The transform that the agent will look at.")]
    public Transform target;

    [Tooltip("The maximum distance that the agent will look at the target.")]
    public float maxDistance = 5.0f;

    [Tooltip("The maximum angle (in degrees) that the agent will turn to look at the target.")]
    public float maxAngle = 60.0f;

    [Tooltip("How quickly the agent will look at the target in seconds.")]
    public float lookTime = 1.0f;

    // The GameObject's aim constraint.
    AimConstraint aimConstraint;

    // The GameObject's initial look rotation.
    Quaternion initialRotation;

    // Start is called before the first frame update
    void Awake()
    {
        // Get the required AimConstraint component.
        aimConstraint = GetComponent<AimConstraint>();

        SetAimConstraintSource();

        // Activate the AimConstraint.
        aimConstraint.constraintActive = true;

        // Set the overall weight to zero.
        aimConstraint.weight = 0.0f;

        // Calculate the GameObject's initial rotation.
        initialRotation = Quaternion.LookRotation(transform.rotation * aimConstraint.aimVector, transform.rotation * aimConstraint.upVector);

        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance to the target.
        float distance = Vector3.Distance(target.position, transform.position);

        // If the target is within the maximum distance,
        if (distance <= maxDistance)
        {
            // Calculate the current look rotation.
            Quaternion currentRotation = Quaternion.LookRotation(transform.TransformPoint(target.position) - transform.position);

            // Calculate the angle between the initial and current rotations.
            float angle = Quaternion.Angle(initialRotation, currentRotation);

            // If the target is within of the maximum angle,
            if (angle <= maxAngle)
            {
                // Add to the AimConstraint's weight.
                aimConstraint.weight += Time.deltaTime / lookTime;
            }
            // If the target is outside the maximum angle,
            else
            {
                // Subtract from the AimConstraint's weight.
                aimConstraint.weight -= Time.deltaTime / lookTime;
            }
        }
        // If the target is outside the maximum distance,
        else
        {
            // Subtract from the AimConstraint's weight (down to 0).
            aimConstraint.weight -= Time.deltaTime / lookTime;
        }

        // Clamp the AimConstraint's weight.
        aimConstraint.weight = Mathf.Clamp(aimConstraint.weight, 0.0f, 1.0f);
    }

    private void OnEnable()
    {
        SetAimConstraintSource();

        aimConstraint.constraintActive = true;

        // Calculate the GameObject's initial rotation.
        Vector3 _startRot = transform.rotation * aimConstraint.aimVector;
        Vector3 _endRot = transform.rotation * aimConstraint.upVector;
        initialRotation = Quaternion.LookRotation(_startRot, _endRot);
    }

    private void OnDisable()
    {
        Quaternion _rot = this.transform.rotation;

        aimConstraint.constraintActive = false;

        this.transform.rotation = _rot;
    }

    private void SetAimConstraintSource()
    {
        // Create a constraint for the target.
        ConstraintSource constraint = new ConstraintSource
        {
            sourceTransform = target,
            weight = 1.0f
        };

        // Create the AimConstraint's sources.
        List<ConstraintSource> sources = new List<ConstraintSource>
        {
            constraint
        };

        // Set the AimConstraint's sources.
        aimConstraint.SetSources(sources);
    }
}
