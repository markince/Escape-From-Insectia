using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsMovement : MonoBehaviour
{
    private Vector3 spriteStartPosition;
    bool movingUp = true;

    // Start is called before the first frame update
    void Start()
    {
        spriteStartPosition = transform.position;

    }





    // Update is called once per frame
    void Update()
    {
        MoveVertical();
    }

    void MoveVertical()
    {
        var position = transform.position;
        if (movingUp == true)
        {
            position.y += 0.01f;
            transform.position = position;
            if (transform.position.y >= spriteStartPosition.y + 0.1f)
            {
                movingUp = false;
            }
        }
        if (movingUp == false)
        {
            position.y -= 0.01f;
            transform.position = position;
            if (transform.position.y <= spriteStartPosition.y - 0.1f)
            {
                movingUp = true;
            }
        }
    }
}
