using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] private List<Light> lights;
    [SerializeField] private float startDelay = 4;
    [SerializeField] private float betweenDelay = 1;

    private bool lightsOn;

    private void Start()
    {
        foreach (Light light in lights)
        {
            light.enabled = false;
        }
    }

    public void TurnOnLights()
    {
        if (!lightsOn)
            StartCoroutine(HandleTurnOnLights());
    }

    private IEnumerator HandleTurnOnLights()
    {
        lightsOn = true;

        yield return new WaitForSeconds(startDelay);

        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].enabled = true;
            yield return new WaitForSeconds(betweenDelay);
        }
    }
}
