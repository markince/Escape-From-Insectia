using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rising water class in tutorial level
public class WaterVerticalScroll2 : MonoBehaviour
{

    [SerializeField] Player player;

    [Tooltip("Game units per second")]
    [SerializeField] float waterScrollRate = 0.2f;
    bool moveWaterUp = false;



    void Update()
    {
        // Hash the player pressed the left or right key?
        // Water starts moving when player moves
        if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Horizontal") > 0)
        {
            moveWaterUp = true;
        }


        if (moveWaterUp == true)
        {
            transform.Translate(Vector2.up * waterScrollRate * Time.deltaTime);
        }

    }
}
