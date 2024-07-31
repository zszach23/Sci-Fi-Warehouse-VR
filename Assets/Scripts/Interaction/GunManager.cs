using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunManager : MonoBehaviour
{
    [SerializeField] InteractionSwitcher interactionSwitcher;
    [SerializeField] HandController controller;

    public bool isPickedUp = false;

    private void Start()
    {
        interactionSwitcher.SwitchToDirectCasting();
    }

    public void OnSelected()
    {
        isPickedUp = true;
        interactionSwitcher.SwitchToRayCasting();

        this.GetComponent<XRGrabInteractable>().enabled = false;
        controller.hand.gameObject.SetActive(false);
        controller.hand = this.GetComponent<Hand>();
        this.GetComponent<Hand>().enabled = true;
        this.GetComponent<Outline>().enabled = false;
    }
}
