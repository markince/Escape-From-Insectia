using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    private bool movingRight = true;
    public Player player;
    public SpiderEnemy spider;
    AudioSource spiderWalking;


    public Transform groundDetection;

    private void Start()
    {
        spiderWalking = GetComponent<AudioSource>();
        spiderWalking.enabled = false;
    }


    void Update()
    {
        Vector3 distanceFromPlayer = spider.transform.position - player.transform.position;

        if (distanceFromPlayer.x < 10.0f || distanceFromPlayer.y < 10.0f)
        {
            spiderWalking.enabled = false;
        }
        else
        {
            spiderWalking.enabled = true;

        }

        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2.0f, LayerMask.GetMask("Ground"));

        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                movingRight = true;
            }

        }
    }

}
