using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractionSwitcher : MonoBehaviour
{
    [SerializeField] private XRDirectInteractor directInteractor;
    [SerializeField] private XRRayInteractor rayInteractor;

    public void SwitchToRayCasting()
    {
        // Disable the direct interactor
        directInteractor.enabled = false;

        // Enable the ray gun's XR Ray Interactor
       rayInteractor.enabled = true;

        // Perform additional setup for ray casting interaction
    }

    public void SwitchToDirectCasting()
    {
        // Disable the direct interactor
        directInteractor.enabled = true;

        // Enable the ray gun's XR Ray Interactor
        rayInteractor.enabled = false;

        // Perform additional setup for ray casting interaction
    }


}
