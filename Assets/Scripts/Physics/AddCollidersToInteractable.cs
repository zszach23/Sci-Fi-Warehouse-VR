using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AddCollidersToInteractable : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable interactable;
    [SerializeField] private BoxCollider[] colliders;

    private void Start()
    {
        colliders = this.GetComponentsInChildren<BoxCollider>();

        for (int i = 0; i < colliders.Length; i++)
        {
            interactable.colliders.Add(colliders[i]);
        }

        StartCoroutine(ReregisterInteractable());
    }

    private IEnumerator ReregisterInteractable()
    {
        yield return new WaitForEndOfFrame();
        interactable.enabled = false;

        yield return new WaitForEndOfFrame();
        interactable.enabled = true;
    }
}
