using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Action taken when player touches exit protal in level 2
public class Level2Exit : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.name == "Player")
        {
            SceneManager.LoadScene(3); // Load level complete scene
        }

    }
}
