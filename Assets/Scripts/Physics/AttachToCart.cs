using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AttachToCart : MonoBehaviour
{
    [SerializeField] private Transform attachPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<XRBaseInteractable>() != null)
        {
            other.transform.SetParent(transform, false);
            FollowAttachPoint attachPointScript = other.AddComponent<FollowAttachPoint>();
            attachPointScript.attachPoint = this.attachPoint;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent <XRBaseInteractable>() != null)
        {
            other.transform.SetParent(null, false);
            Destroy(other.GetComponent<FollowAttachPoint>());
        }
    }
}
