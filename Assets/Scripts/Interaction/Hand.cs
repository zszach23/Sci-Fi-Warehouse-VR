using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Animator))]

public class Hand : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private bool animate = true;
    private Animator animator;
    private SkinnedMeshRenderer mesh;

    private string gripAnimationParameter = "Grip";
    private string triggerAnimationParameter = "Trigger";

    [SerializeField] private float animationSpeed;

    private float gripCurrent;
    private float triggerCurrent;

    private float gripTarget;
    private float triggerTarget;

    [Header("Physics Movement")]

    [SerializeField] private ActionBasedController controller;

    [Space]

    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotateSpeed = 100f;

    [Space]

    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;

    [Header("Grabbing")]
    [SerializeField] private bool directGrabbing = true;
    [SerializeField] private Transform palm;
    [SerializeField] private float reachDistance = .1f;
    [SerializeField] private float joinDistance = .05f;
    [SerializeField] LayerMask grabbableLayer;

    private Transform followTarget;
    private Rigidbody rb;

    private bool isGrabbing;
    private GameObject heldObject;
    private Rigidbody heldObjectRB;
    private Transform grabPoint;

    private FixedJoint joint1;
    private FixedJoint joint2;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        mesh = this.GetComponentInChildren<SkinnedMeshRenderer>();

        followTarget = controller.gameObject.transform;

        rb = this.GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.mass = 20f;
        rb.maxAngularVelocity = 20f;

        // Input Setup
        controller.selectAction.action.started += Grab;
        controller.selectAction.action.canceled += Release;

        rb.position = followTarget.position;
        rb.rotation = followTarget.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
    }

    private void FixedUpdate()
    {
        PhysicsMove();
    }

    private void OnDisable()
    {
        controller.selectAction.action.started -= Grab;
        controller.selectAction.action.canceled -= Release;
    }

    private void Grab(InputAction.CallbackContext context)
    {
        if (!directGrabbing)
            return;

        if (isGrabbing || heldObject)
            return;

        Collider[] colliders = Physics.OverlapSphere(palm.position, reachDistance, grabbableLayer);

        if (colliders.Length <= 0)
            return;

        GameObject objectToGrab = colliders[0].transform.gameObject;

        // Object not on the direct interactable interaction layer
        //if (objectToGrab.GetComponentInParent<XRGrabInteractable>().interactionLayers.value != LayerMask.NameToLayer("DirectInteractable"))
        //{
        //    return;
        //}

        Rigidbody objectBody = objectToGrab.GetComponent<Rigidbody>();

        if (objectBody != null)
        {
            heldObject = objectBody.gameObject;
        }
        else
        {
            objectBody = objectToGrab.GetComponentInParent<Rigidbody>();

            if (objectBody != null)
            {
                heldObject = objectBody.gameObject;
            }
            else
            {
                return;
            }
        }

        StartCoroutine(GrabObject(colliders[0], objectBody));
    }

    private IEnumerator GrabObject(Collider collider, Rigidbody objectBody)
    {
        isGrabbing = true;

        // Create Grab Point
        grabPoint = new GameObject().transform;
        grabPoint.position = collider.ClosestPoint(palm.position);
        grabPoint.parent = heldObject.transform;

        // Move Hand to Grab Point
        followTarget = grabPoint;

        // Wait for hand to reach Grab point
        while (grabPoint != null && Vector3.Distance(grabPoint.position, palm.position) > joinDistance && isGrabbing)
        {
            yield return new WaitForEndOfFrame();
        }

        // Freeze hand and object motion
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        objectBody.velocity = Vector3.zero;
        objectBody.angularVelocity = Vector3.zero;

        objectBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        objectBody.interpolation = RigidbodyInterpolation.Interpolate;

        heldObjectRB = objectBody;

        // Attach Joints
        joint1 = this.gameObject.AddComponent<FixedJoint>();
        joint1.connectedBody = objectBody;
        joint1.breakForce = float.PositiveInfinity;
        joint1.breakTorque = float.PositiveInfinity;

        joint1.connectedMassScale = 1;
        joint1.massScale = 1;
        joint1.enableCollision = false;
        joint1.enablePreprocessing = false;

        joint2 = objectBody.gameObject.AddComponent<FixedJoint>();
        joint2.connectedBody = rb;
        joint2.breakForce = float.PositiveInfinity;
        joint2.breakTorque = float.PositiveInfinity;

        joint2.connectedMassScale = 1;
        joint2.massScale = 1;
        joint2.enableCollision = false;
        joint2.enablePreprocessing = false;


        // Reset Follow target
        followTarget = controller.gameObject.transform;
    }

    private void Release(InputAction.CallbackContext context)
    {
        if (!directGrabbing)
            return;
        
        if (joint1 != null)
            Destroy(joint1);

        if (joint2 != null)
        {
            Debug.Log("Release");
            Destroy(joint2);
        }
            

        if (grabPoint != null)
            Destroy(grabPoint.gameObject);

        if (heldObject != null)
        {
            Rigidbody objectBody = heldObject.GetComponent<Rigidbody>();
            objectBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            objectBody.interpolation = RigidbodyInterpolation.None;
            objectBody.velocity = Vector3.zero;
            objectBody.angularVelocity = Vector3.zero;

            heldObject = null;
            heldObjectRB = null;
        }

        isGrabbing = false;

        if (controller != null)
            followTarget = controller.gameObject.transform;
    }

    void PhysicsMove()
    {
        // Position
        Vector3 positionWithOffset = followTarget.TransformPoint(positionOffset);
        float distance = Vector3.Distance(this.transform.position, positionWithOffset);
        rb.velocity = (positionWithOffset - this.transform.position).normalized * followSpeed * distance;

        // Rotation
        Quaternion rotationWithOffset = followTarget.rotation * Quaternion.Euler(rotationOffset);
        Quaternion targetRotation = rotationWithOffset * Quaternion.Inverse(rb.rotation);
        targetRotation.ToAngleAxis(out float angle, out Vector3 axis);
        rb.angularVelocity = axis * angle * Mathf.Deg2Rad * rotateSpeed;

        if (heldObjectRB != null)
        {
            heldObjectRB.velocity = rb.velocity;
            heldObjectRB.angularVelocity = rb.angularVelocity;
        }
    }

    internal void SetGrip(float v)
    {
        gripTarget = v;
    }

    internal void SetTrigger(float v)
    {
        triggerTarget = v;
    }

    void AnimateHand()
    {
        if (!animate)
        {
            return;
        }

        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(gripAnimationParameter, gripCurrent);
        }
        
        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(triggerAnimationParameter, triggerCurrent);
        }
    }

    public void ToggleVisibility()
    {
        mesh.enabled = !mesh.enabled;

        Collider[] handColliders = this.GetComponentsInChildren<Collider>();

        foreach (Collider collider in handColliders)
        {
            collider.enabled = !collider.enabled;
        }
    }
}
