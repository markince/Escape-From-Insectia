using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinkBeePathing : MonoBehaviour
{
    public int pinkBeeHealth = 100;
    private Animator animator;
    public int pointsAwardedWhenKilled = 100;
    public Text playerScoreText;
    public AudioClip deathSound;
    public GameObject deathEffect;


    [SerializeField] BeeWaveConfig waveConfig;
    List<Transform> waypoints;
    [SerializeField] float moveSpeed = 2.0f;

    int wayPointIndex = 0; // Which waypoint is the bee currently moving towards?

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
        animator = GetComponent<Animator>();


        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[wayPointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBee();
    }

    private void MoveBee()
    {
        if (wayPointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[wayPointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            if (transform.position == targetPosition)
            {
                wayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(HitAnimation(0.5f));

        pinkBeeHealth -= damage;
        CombatTextManager.Instance.CreateText(transform.position, "-" + damage.ToString(), Color.white);


        if (pinkBeeHealth <= 0)
        {
            Die();


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
}
