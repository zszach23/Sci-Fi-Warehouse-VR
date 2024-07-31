using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip buttonPress;

    [Range(0f, 1f)]
    [SerializeField] private float volume;

    public void QuitApplication()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void RestartApplication()
    {
        SoundManager.instance.PlaySound(buttonPress, this.transform, volume);

        LevelManager.instance.FadeToLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
