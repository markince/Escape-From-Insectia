using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGemPickup : MonoBehaviour
{
    [SerializeField] AudioClip blueGemPickupSFX = null;
    [SerializeField] int pointsPerBlueGemPickup = 200;

    // This functon prevents the audioclip being played only in one speaker 
    // (this is becuase the cinemachine camera lags behind the player and the
    // sound effect is played at the cameras position
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get the correct collision box of the player for the gem to collide with
        var playerBody = FindObjectOfType<Player>().GetComponent<CapsuleCollider2D>();

        // Check to see if the correct collider has collider with the gem
        // This stops the gem from incrementing by more than 1
        if (collision == playerBody)
        {
            // Increase number of collectable gems collected and display result to UI
            FindObjectOfType<GameSession>().IncrementBlueGems(pointsPerBlueGemPickup);

            // PLay gem pickup sound
            PlayAudioClipAtPoint(Camera.main.transform.position, 0.0F, blueGemPickupSFX);

            CombatTextManager.Instance.CreateText(transform.position, "+" + pointsPerBlueGemPickup.ToString(), Color.yellow);

            // Destroy this game object.
            Destroy(gameObject);


        }

    }



}
