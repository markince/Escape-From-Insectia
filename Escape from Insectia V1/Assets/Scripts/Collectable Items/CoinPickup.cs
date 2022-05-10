﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{


    [SerializeField] AudioClip coinPickupSFX = null;
    [SerializeField] int pointsPerCoinPickup = 50;

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
        // Get the correct collision box of the player for the coin to collide with
        var playerBody = FindObjectOfType<Player>().GetComponent<CapsuleCollider2D>();

        // Check to see if the correct collider has collider with the coin
        // This stops the coin from incrementing by more than 1
        if (collision == playerBody)
        {
            // Increase number of coins collected and display result to UI
            FindObjectOfType<GameSession>().IncrementCoins(pointsPerCoinPickup);


            CombatTextManager.Instance.CreateText(transform.position, "+" + pointsPerCoinPickup.ToString(), Color.yellow);


            // PLay coin pickup sound
            PlayAudioClipAtPoint(Camera.main.transform.position, 0.0F, coinPickupSFX);

            // Destroy this game object.
            Destroy(gameObject);
        }



    }





}
