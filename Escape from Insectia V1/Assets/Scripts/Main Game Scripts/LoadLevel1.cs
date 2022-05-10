using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LoadLevel1 : MonoBehaviour
{
    [SerializeField] public AudioClip clip;

    // This function plays a sound directly at the cameras position so it sounds closer to the user
    public AudioSource PlayAudioClipAtPoint(Vector3 position, float spatialBlend, AudioClip audioClip)
    {
        GameObject tempAudioClip = new GameObject("TmpAudio");
        tempAudioClip.transform.position = position;
        AudioSource audio_source = tempAudioClip.AddComponent<AudioSource>();
        audio_source.spatialBlend = spatialBlend;         // Set the spatial blend
        audio_source.clip = audioClip;
        audio_source.Play();
        Destroy(tempAudioClip, audioClip.length); // Destroy the game object after clip has finised playing
        return audio_source;
    }

    // Load level 1
    public void LoadLevel()
    {
        PlayAudioClipAtPoint(Camera.main.transform.position, 0.0F, clip);

        SceneManager.LoadScene(2);

    }
}
