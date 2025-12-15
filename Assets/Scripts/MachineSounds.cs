using UnityEngine;
using System.Collections;

public class MachineSounds : MonoBehaviour
{
    public bool sonidoReproduciendose;
    private AudioSource audioSource;
    private bool coroutineActiva;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (!coroutineActiva)
        {
            StartCoroutine(CicloAudio());
        }
    }

    IEnumerator CicloAudio()
    {
        coroutineActiva = true;

        while (true)
        {
            // Reproducir audio
            audioSource.Play();
            sonidoReproduciendose = true;

            yield return new WaitForSeconds(30f);

            // Parar audio (NO desactivar componente)
            audioSource.Stop();
            sonidoReproduciendose = false;

            yield return new WaitForSeconds(15f);
        }
    }
}
