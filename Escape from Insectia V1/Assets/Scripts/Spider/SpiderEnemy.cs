using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderEnemy : MonoBehaviour
{
    [SerializeField] GameObject particleExplosion;

    public int spiderHealth = 100;
    private Animator animator;
    public int pointsAwardedWhenKilled = 100;
    public Text playerScoreText;

    public AudioClip deathSound;

    public GameObject deathEffect;

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

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void TakeDamage (int damage)
    {
        StartCoroutine(HitAnimation(0.5f));

        spiderHealth -= damage;
        CombatTextManager.Instance.CreateText(transform.position, "-" + damage.ToString(), Color.white);


        if (spiderHealth <= 0)
        {
            Die();
            TriggerExplosionParticleFX();


            GameSession.playerScore += pointsAwardedWhenKilled;
            //playerScoreText.text = GameSession.playerScore.ToString();

            CombatTextManager.Instance.CreateText(transform.position, "+" + pointsAwardedWhenKilled.ToString(), Color.yellow);


            //StartCoroutine(AwardPointsToPlayer(pointsAwardedWhenKilled));
        }



        void Die()
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            PlayAudioClipAtPoint(transform.position, 0.0f, deathSound);
            Destroy(gameObject);
        }
    }

    IEnumerator AwardPointsToPlayer(int numOfPoints)
    {
        GameSession.playerScore += numOfPoints;
        yield return new WaitForSeconds(1);
        CombatTextManager.Instance.CreateText(transform.position, numOfPoints.ToString(), Color.yellow);
    }



    IEnumerator HitAnimation(float time)
    {

        animator.SetBool("isHit", true);
        yield return new WaitForSeconds(time);
        animator.SetBool("isHit", false);



    }

    private void TriggerExplosionParticleFX()
    {
        GameObject explosion = Instantiate(particleExplosion, transform.position, Quaternion.identity);
    }
}
