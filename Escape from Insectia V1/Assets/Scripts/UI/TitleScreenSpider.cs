using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum spiderMovement
{
    positionTop,
    movingDown,
    positionLeft,
    movingLeft
}
public class TitleScreenSpider : MonoBehaviour
{
    float speed = 300.0f;
    spiderMovement movement;
    Vector3 startPosition = new Vector3 ( 360.0f, 1200.0f, 0.0f );

    // Start is called before the first frame update
    void Start()
    {
        movement = spiderMovement.movingDown;
        transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (movement == spiderMovement.movingDown)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }



    }
}
