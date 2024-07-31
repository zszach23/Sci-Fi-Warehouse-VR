using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    [SerializeField] private bool isDay;
    [Space]
    [SerializeField] private Material daySkyboxMaterial;
    [SerializeField] private Material nightSkyboxMaterial;
    [Space]
    [SerializeField] private float transitionTime;

    [Header("Audio")]
    [SerializeField] private AudioClip buttonPress;

    [Range(0f, 1f)]
    [SerializeField] private float volume;

    private bool _isAnimating;

    public void ToggleSkybox()
    {
        if (!_isAnimating)
        {
            StartCoroutine(IToggleSkybox());
        }
    }

    private IEnumerator IToggleSkybox()
    {
        _isAnimating = true;

        Material currSkybox = RenderSettings.skybox;
        Material targetSkybox = isDay ? nightSkyboxMaterial : daySkyboxMaterial;

        float _elapsedTime = 0;

        while (_elapsedTime < transitionTime)
        {
            RenderSettings.skybox.Lerp(currSkybox, targetSkybox, _elapsedTime / transitionTime);
            DynamicGI.UpdateEnvironment();
            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        RenderSettings.skybox = targetSkybox;
        DynamicGI.UpdateEnvironment();

        isDay = !isDay;

        _isAnimating = false;
    }
}
