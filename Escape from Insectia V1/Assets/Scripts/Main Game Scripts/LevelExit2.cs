using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// End of first level portal
public class LevelExit2 : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.name == "Player")
        {
            SceneManager.LoadScene(3); // Load level complete screen

        }

    }
}
