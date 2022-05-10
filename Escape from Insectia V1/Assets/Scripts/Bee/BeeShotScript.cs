using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeShotScript : MonoBehaviour
{
    [SerializeField] AudioClip fireBulletSFX = null;
    public float speed = 8.0f;
    BoxCollider2D myBoxCollider;
    Rigidbody2D myRigidBody;
    public int damageAmount = 0;
    public Player player;
    
    public GameObject bee;
    public GameObject bulletPrefab;
    public GameObject impactEffect;

    bool hitCheck = false;

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

    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();




    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);




        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Hazards")))
        {
            hitCheck = true;

        }




        if (hitCheck)
        {
            GameObject clone = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);

            transform.position = bee.transform.position;
            hitCheck = false;

            if (player.transform.position.y > 92.5f)
            {
                PlayAudioClipAtPoint(Camera.main.transform.position, 0.0F, fireBulletSFX);

            }

            Destroy(clone, 1.0f);
        }


    }



}