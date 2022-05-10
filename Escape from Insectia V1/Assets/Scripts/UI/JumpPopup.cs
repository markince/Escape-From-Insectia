using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPopup : MonoBehaviour
{
    [SerializeField] AudioClip popupSFX = null;
    [SerializeField] AudioClip hideSFX = null;
    BoxCollider2D playerInteractionBox;

    private bool hasPlayedPopupSFX = false;
    private bool hasPlayedHideSFX = false;

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
    void Start()
    {
        playerInteractionBox = GetComponent<BoxCollider2D>();
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {


            StartCoroutine(DisplayplayJumpTips());


            IEnumerator DisplayplayJumpTips()
            {
                // Display popup tip
                this.GetComponent<SpriteRenderer>().enabled = true;

                // Play popup tip SFX
                if (!hasPlayedPopupSFX)
                {
                    PlayAudioClipAtPoint(Camera.main.transform.position, 0.0F, popupSFX);
                    hasPlayedPopupSFX = true;
                }

                // Wait 3 seconds
                yield return new WaitForSeconds(3);

                // Hide popup tip
                this.GetComponent<SpriteRenderer>().enabled = false;

                // Play hide tip SFX
                if (!hasPlayedHideSFX)
                {
                    PlayAudioClipAtPoint(Camera.main.transform.position, 0.0F, hideSFX);
                    hasPlayedHideSFX = true;
                }

                // Wait 3 seconds
                yield return new WaitForSeconds(1);

                hasPlayedPopupSFX = false;
                hasPlayedHideSFX = false;

            }

        }

    }

}
