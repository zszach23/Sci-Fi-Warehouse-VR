using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleTunnelVignette : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private GameObject vignette;
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        var toggle = actionAsset.FindActionMap("XRI LeftHand Interaction").FindAction("Toggle Vignette");
        toggle.Enable();
        toggle.performed += OnToggleVignette;
    }

    private void OnToggleVignette(InputAction.CallbackContext context)
    {
        if (isActive)
        {
            vignette.SetActive(false);
            isActive = false;
        }
        else
        {
            vignette.SetActive(true);
            isActive = true;
        }
    }
}
