using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource soundObjectPrefab;

    [Header("Gravity Gun")]
    [SerializeField] private AudioSource activateGun;
    [Space]
    [SerializeField] private AudioSource pickupGun;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ShootBeam()
    {
        activateGun.Play();
    }

    public void DisableBeam()
    {
        activateGun.Stop();
    }

    public void PickUpGun()
    {
        pickupGun.Play();
    }

    public void PlaySound(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // Spawn GameObject
        AudioSource audioSource = Instantiate(soundObjectPrefab, spawnTransform.position, Quaternion.identity);

        // Assign Audio Clip
        audioSource.clip = audioClip;

        // Assign volume
        audioSource.volume = volume;

        // Play Sound
        audioSource.Play();

        // Get Length of Sound Clip
        float clipLength = audioSource.clip.length;

        // Destroy Clip After It Is Done Playing
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSound(AudioClip[] audioClips, Transform spawnTransform, float volume)
    {
        // Assign Random Index
        int randomIndex = Random.Range(0, audioClips.Length);

        PlaySound(audioClips[randomIndex], spawnTransform, volume);
    }
}
