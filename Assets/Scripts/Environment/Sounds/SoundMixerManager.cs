using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    private string masterVolumeParameter = "MasterVolume";
    private string soundFXVolumeParameter = "SoundFXVolume";
    private string ambienceVolumeParameter = "AmbienceVolume";

    /*** Note: Decibels are logarithmic ***/

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat(masterVolumeParameter, Mathf.Log10(level) * 20f);
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat(soundFXVolumeParameter, Mathf.Log10(level) * 20f);
    }

    public void SetAmbienceVolume(float level)
    {
        audioMixer.SetFloat(ambienceVolumeParameter, Mathf.Log10(level) * 20f);
    }
}
