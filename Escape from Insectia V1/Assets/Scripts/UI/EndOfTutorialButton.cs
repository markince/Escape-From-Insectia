using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfTutorialButton : MonoBehaviour
{
    [SerializeField] public AudioClip clip;
    public GameObject endOfLevelPanel;
    public Player player;


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

    public void GoBackToMainMenu()
    {
        PlayAudioClipAtPoint(Camera.main.transform.position, 0.0F, clip);
        endOfLevelPanel.SetActive(false);
        SceneManager.LoadScene(0);
    }



}
