using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtCamera : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(mainCamera.transform);
    }
}
