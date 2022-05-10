using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTutorialButton : MonoBehaviour
{
    [SerializeField] GameObject tutorialStartPanel;
    [SerializeField] GameObject scorePanel;
    [SerializeField] GameObject clockPanel;
    [SerializeField] GameObject playerPanel;
    [SerializeField] GameObject inventoryPanel;

    [SerializeField] public AudioClip clip;

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

    // Start is called before the first frame update

    public void StartGame()
    {

        PlayAudioClipAtPoint(Camera.main.transform.position, 0.0F, clip);
        tutorialStartPanel.transform.position = new Vector3(100.0f, 0.0f, 0.0f);
        scorePanel.SetActive(true);
        clockPanel.SetActive(true);
        playerPanel.SetActive(true);
        inventoryPanel.SetActive(true);
        Time.timeScale = 1;

    }

}
