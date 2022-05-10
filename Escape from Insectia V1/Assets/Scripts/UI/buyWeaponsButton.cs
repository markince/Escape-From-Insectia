using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyWeaponsButton : MonoBehaviour
{
    [SerializeField] public AudioClip clip;
    [SerializeField] public GameObject storeBackground;
    [SerializeField] public GameObject weaponsBackground;

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
        storeBackground.SetActive(false);
        weaponsBackground.SetActive(true);

    }


}
