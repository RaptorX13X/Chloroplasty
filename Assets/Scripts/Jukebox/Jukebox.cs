using System.Collections;
using UnityEngine;

public class Jukebox : MonoBehaviour
{
    [SerializeField] private AudioSource jazzMusic;
    private bool jazzPlaying;
    [SerializeField] private AudioSource medievalMusic;
    private bool medievalPlaying;
    [SerializeField] private AudioSource synthMusic;
    private bool synthPlaying;
    [SerializeField] private AudioSource discoMusic;
    private bool discoPlaying;
    [SerializeField] private AudioSource lofiMusic;
    private bool lofiPlaying;
    [SerializeField] private AudioSource rockMusic;
    private bool rockPlaying;

    private IEnumerator StopAllMusic()
    {
        jazzMusic.Stop();
        medievalMusic.Stop();
        synthMusic.Stop();
        discoMusic.Stop();
        lofiMusic.Stop();
        rockMusic.Stop();
        jazzPlaying = false;
        medievalPlaying = false;
        synthPlaying = false;
        discoPlaying = false;
        lofiPlaying = false;
        rockPlaying = false;
        yield return new WaitForSeconds(0.5f);
    }
    public void JazzButton()
    {
        if (jazzPlaying) return;
        StartCoroutine(StopAllMusic());
        jazzMusic.Play();
        jazzPlaying = true;
    }

    public void MedievalButton()
    {
        if (medievalPlaying) return;
        StartCoroutine(StopAllMusic());
        medievalPlaying = true;
        medievalMusic.Play();
    }

    public void SynthButton()
    {
        if (synthPlaying) return;
        StartCoroutine(StopAllMusic());
        synthPlaying = true;
        synthMusic.Play();
    }

    public void DiscoButton()
    {
        if (discoPlaying) return;
        StartCoroutine(StopAllMusic());
        discoPlaying = true;
        discoMusic.Play();
    }

    public void RockButton()
    {
        if (rockPlaying) return;
        StartCoroutine(StopAllMusic());
        rockPlaying = true;
        rockMusic.Play();
    }

    public void LofiButton()
    {
        if (lofiPlaying) return;
        StartCoroutine(StopAllMusic());
        lofiPlaying = true;
        lofiMusic.Play();
    }
}
