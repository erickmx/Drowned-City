using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnAudio : MonoBehaviour
{
    // Atributos de audio, boton y evento click.
    public AudioSource audioSource;
    public AudioClip clip;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        audioSource.clip = clip;
    }

    // Reproducir audio.
    public void Reproducir()
    {
        audioSource.Play();
    }
}