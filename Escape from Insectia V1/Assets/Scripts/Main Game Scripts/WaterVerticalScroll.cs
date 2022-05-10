using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Function to action vertical scrolling water on level 1
public class WaterVerticalScroll : MonoBehaviour
{

     [SerializeField] Player player;

     [Tooltip ("Game units per second")]
     [SerializeField] float waterScrollRate = 0.2f;
     bool moveWaterUp = false;

 
     void Update()
     {
        // Has the player standing still and above the required position in the game?
        if ((Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Horizontal") > 0) && player.transform.position.y > 70.0f)
        {
            moveWaterUp = true;
        }


        if (moveWaterUp == true)
        {
            transform.Translate(Vector2.up * waterScrollRate * Time.deltaTime);
        }

     }

}
