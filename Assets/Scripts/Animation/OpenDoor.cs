using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [Header("Height Change")]
    [SerializeField] private float endHeight = 2.6f;

    [Header("Animation Timing")]
    [SerializeField] private bool isInAnimation = false;
    [SerializeField] private float delayDoorOpen = 1;
    [SerializeField] private float openDoorTime = 1;
    [SerializeField] private float delayDoorClose = 5;

    [Header("Sounds")]
    [SerializeField] private AudioClip doorMoving;

    [Range(0f, 1f)]
    [SerializeField] private float volume;

    [Header("Debug Info")]
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;

    private void Start()
    {
        startPosition = transform.position;
        endPosition = transform.position + new Vector3(0, endHeight, 0);
    }

    public void HandleOpenDoor()
    {
        if (!isInAnimation)
            StartCoroutine(AnimateDoor());
    }

    public IEnumerator AnimateDoor()
    {
        isInAnimation = true;

        float elapsedTime = 0;

        yield return new WaitForSeconds(delayDoorOpen);

        SoundManager.instance.PlaySound(doorMoving, this.transform, volume);

        while (elapsedTime < openDoorTime)
        {
            this.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(delayDoorClose);

        elapsedTime = 0;

        SoundManager.instance.PlaySound(doorMoving, this.transform, volume);

        while (elapsedTime < openDoorTime)
        {
            this.transform.position = Vector3.Lerp(endPosition, startPosition, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isInAnimation = false;
    }
}
