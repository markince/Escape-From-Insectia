using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelMusic : MonoBehaviour
{
    [SerializeField] public AudioSource levelMusic;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        levelMusic = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (levelMusic.isPlaying) return;
        levelMusic.Play();
    }

    public void StopMusic()
    {
        levelMusic.Stop();
    }
}
