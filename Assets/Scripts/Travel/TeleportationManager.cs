using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private TeleportationProvider teleportationProvider;

    private InputAction thumbstick;
    private bool isActive;

    void Start()
    {
        if (rayInteractor != null)
            rayInteractor.enabled = false;

        InputAction activate = actionAsset.FindActionMap("XRI RightHand Locomotion").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += OnTeleportActivate;

        InputAction cancel = actionAsset.FindActionMap("XRI RightHand Locomotion").FindAction("Teleport Mode Cancel");
        cancel.Enable();
        cancel.performed += OnTeleportCancel;

        thumbstick = actionAsset.FindActionMap("XRI RightHand Locomotion").FindAction("Move");
        thumbstick.Enable();
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }

        if (thumbstick.triggered)
        {
            return;
        }

        if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            if (rayInteractor != null)
                rayInteractor.enabled = false;
            isActive = false;
            return;
        }

        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = hit.point
        };

        teleportationProvider.QueueTeleportRequest(request);

        if (rayInteractor != null)
            rayInteractor.enabled = false;
        isActive = false;


    }

    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        isActive = true;
        if (rayInteractor != null)
            rayInteractor.enabled = true;

    }

    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        isActive = false;
        if (rayInteractor != null)
            rayInteractor.enabled = false;
    }
}
