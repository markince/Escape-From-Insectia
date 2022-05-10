using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Plays the ingame music
public class StartMenuMusic : MonoBehaviour
{
    [SerializeField] public AudioSource introlevelMusic;

    private void Awake()
    {
        introlevelMusic = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (introlevelMusic.isPlaying) return;
        introlevelMusic.Play();
    }

    public void StopMusic()
    {
        introlevelMusic.Stop();
    }
}
