using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMover : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
    }

    public void MoveTo(Vector3 pos)
    {
        rb.MovePosition(pos);
    }
}
