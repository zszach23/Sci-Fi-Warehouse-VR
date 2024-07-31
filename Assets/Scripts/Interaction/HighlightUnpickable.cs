using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightUnpickable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Raycastable"))
        {
            other.gameObject.GetComponentInParent<InteractionOutlineManager>().OutlineRed();
            other.gameObject.GetComponentInParent<InteractionOutlineManager>().EnableOutline();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Raycastable"))
        {
            other.gameObject.GetComponentInParent<InteractionOutlineManager>().OutlineGreen();
            other.gameObject.GetComponentInParent<InteractionOutlineManager>().DisableOutline();
        }
    }
}
