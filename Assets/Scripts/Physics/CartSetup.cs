using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CartSetup : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private XRBaseInteractable handle;
    [SerializeField] private PhysicsMover mover;

    private Vector3 grabPosition = Vector3.zero;
    //private float startingPercentage = 0;
    //private float currentPercentage = 0;

    private void Update()
    {
        if (handle.isSelected)
            UpdateCart();
    }

    private void UpdateCart()
    {

    }

    private void OnEnable()
    {
        handle.selectEntered.AddListener(StoreGrabInfo);
    }

    private void OnDisable()
    {
        handle.selectEntered.RemoveListener(StoreGrabInfo);   
    }

    private void StoreGrabInfo(SelectEnterEventArgs args)
    {
        grabPosition = args.interactorObject.transform.position;
    }
}
