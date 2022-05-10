using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewStore : MonoBehaviour
{
    [SerializeField] public AudioClip clip;
    [SerializeField] public GameObject mainMenuBackground;
    [SerializeField] public GameObject mainTitle;
    [SerializeField] public GameObject mainShopBackground;



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


    public void OnButtonClick()
    {
        PlayAudioClipAtPoint(Camera.main.transform.position, 0.0F, clip);
        mainMenuBackground.SetActive(false);
        mainTitle.SetActive(false);

        mainShopBackground.SetActive(true);

    }


}
