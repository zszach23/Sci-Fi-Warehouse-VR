using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleUI : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private GameObject UIMenu;
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        var toggle = actionAsset.FindActionMap("XRI LeftHand Interaction").FindAction("Toggle UI");
        toggle.Enable();
        toggle.performed += OnToggleVignette; // Forgot to change lol
    }

    private void OnToggleVignette(InputAction.CallbackContext context)
    {
        if (isActive)
        {
            UIMenu.SetActive(false);
            isActive = false;
        }
        else
        {
            UIMenu.SetActive(true);
            isActive = true;
        }
    }
}
