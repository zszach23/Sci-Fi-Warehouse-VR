using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAttachPoint : MonoBehaviour
{
    public Transform attachPoint;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void Update()
    {
        this.transform.position = attachPoint.position;
        this.transform.rotation = attachPoint.rotation;
    }

    private void OnDestroy()
    {
        rb.isKinematic = false;
    }
}
