using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictMovement : MonoBehaviour
{
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Quaternion startRot;

    [SerializeField] private bool xPos;
    [SerializeField] private bool yPos;
    [SerializeField] private bool zPos;
    [SerializeField] private bool xRot;
    [SerializeField] private bool yRot;
    [SerializeField] private bool zRot;

    private void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (xPos)
        {
            transform.position = new Vector3(startPos.x, transform.position.y, transform.position.z);
        }

        if (yPos)
        {
            transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
        }

        if (zPos)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startPos.z);
        }

        if (xRot)
        {
            transform.rotation = Quaternion.Euler(startRot.eulerAngles.x, transform.rotation.y, transform.rotation.z);
        }

        if (yRot)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, startRot.eulerAngles.y, transform.rotation.z);
        }

        if (zRot)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, startRot.eulerAngles.z);
        }
    }
}
