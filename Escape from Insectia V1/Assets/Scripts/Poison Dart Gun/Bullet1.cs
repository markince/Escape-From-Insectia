using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    [SerializeField] AudioClip fireBulletSFX = null;
    public float speed = 20.0f;
    BoxCollider2D myBoxCollider;
    Rigidbody2D myRigidBody;
    public int damageAmount = 0;
    public GameObject impactEffect;



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


    // Start is called when the bullet is created
    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();

        PlayAudioClipAtPoint(Camera.main.transform.position, 0.0F, fireBulletSFX);

        myRigidBody.velocity = transform.right * speed;

    }

    void Update()
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            GameObject clone = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
            Destroy(clone, 1.0f);
        }


    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Is the bullet hiting an enemy?
        if (hitInfo.gameObject.name == "PatrolingSpider1" || hitInfo.gameObject.name == "PatrolingSpider2" ||
            hitInfo.gameObject.name == "PatrolingSpider3" || hitInfo.gameObject.name == "PatrolingSpider4")
        {
            SpiderEnemy spider = hitInfo.GetComponent<SpiderEnemy>();

            if (spider != null)
            {
                damageAmount = Random.Range(15, 25);

                spider.TakeDamage(damageAmount);
            }

            GameObject clone = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
            Destroy(clone, 1.0f);
        }

        // Is the bullet hiting an enemy?
        if (hitInfo.gameObject.name == "Pink Bee(Clone)")
        {
            PinkBeePathing pinkBee = hitInfo.GetComponent<PinkBeePathing>();

            if (pinkBee != null)
            {
                damageAmount = Random.Range(15, 25);

                pinkBee.TakeDamage(damageAmount);
            }

            GameObject clone = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
            Destroy(clone, 1.0f);
        }

        if (hitInfo.gameObject.name == "ArmouredBee(Clone)")
        {
            PinkBeePathing armouredBee = hitInfo.GetComponent<PinkBeePathing>();

            if (armouredBee != null)
            {
                damageAmount = Random.Range(15, 25);

                armouredBee.TakeDamage(damageAmount);
            }

            GameObject clone = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
            Destroy(clone, 1.0f);
        }

    }





    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
