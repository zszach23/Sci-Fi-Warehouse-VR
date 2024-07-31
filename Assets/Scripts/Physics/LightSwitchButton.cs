using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightSwitchButton : MonoBehaviour
{
    public UnityEvent onPressed;
    public UnityEvent onReleased;

    [SerializeField] private float threshold = .1f;
    [SerializeField] private float deadZone = .025f;

    [SerializeField] private MeshRenderer buttonMesh;
    [SerializeField] private Material buttonPressedMaterial;
    [SerializeField] private Material buttonReleasedMaterial;

    private bool isPressed;
    private Vector3 startPosition;
    private ConfigurableJoint joint;

    [Header("Audio")]
    [SerializeField] private AudioClip buttonPressAudio;

    [Range(0f, 1f)]
    [SerializeField] private float volume;

    private void Start()
    {
        startPosition = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
        buttonMesh = GetComponentInChildren<MeshRenderer>();
        buttonMesh.material = buttonReleasedMaterial;
    }

    private void Update()
    {
        if (!isPressed && GetValue() + threshold >= 1)
        {
            Pressed();
        }

        if (isPressed && GetValue() - threshold <= 0)
        {
            Released();
        }
    }

    private float GetValue()
    {
        float value = Vector3.Distance(startPosition, transform.localPosition) / joint.linearLimit.limit;

        if (Mathf.Abs(value) < deadZone)
        {
            value = 0;
        }

        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        isPressed = true;
        onPressed.Invoke();
        buttonMesh.material = buttonPressedMaterial;
    }

    private void Released()
    {
        isPressed = false;
        onReleased.Invoke();
        buttonMesh.material = buttonReleasedMaterial;
    }

    public void PlayButtonPressAudio()
    {
        SoundManager.instance.PlaySound(buttonPressAudio, this.transform, volume);
    }
}
