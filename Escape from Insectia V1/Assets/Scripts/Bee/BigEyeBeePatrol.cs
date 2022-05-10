using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEyeBeePatrol : MonoBehaviour
{
    public float speed;
    private bool movingRight = true;

    enum EnemyMovement { MovingRight, MovingLeft };
    EnemyMovement beeMovement = EnemyMovement.MovingRight;

    private void Start()
    {
    }

    void Update()
    {


        switch (beeMovement)
        {
            case EnemyMovement.MovingRight:
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    transform.position += new Vector3(1.0f, 0.0f, 0.0f) * speed * Time.deltaTime;
                    if (transform.position.x > 29.0f)
                    {

                        movingRight = false;
                        beeMovement = EnemyMovement.MovingLeft;
                    }
                    break;
                }
            case EnemyMovement.MovingLeft:
                {
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                    transform.position += new Vector3(-1.0f, 0.0f, 0.0f) * speed * Time.deltaTime;
                    if (transform.position.x < 7.0f)
                    {
                        movingRight = true;
                        beeMovement = EnemyMovement.MovingRight;
                    }

                    break;
                }

        }



       /*if (movingRight == true)
        {
            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        }*/
    }
}
