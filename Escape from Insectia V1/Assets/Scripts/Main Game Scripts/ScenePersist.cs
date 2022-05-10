using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Scene Persist Class
// This is another singleton pattern class that will make sure objects collected in the game will not reset when the player dies
public class ScenePersist : MonoBehaviour
{
    int startSceneIndex = 0;

    private void Awake()
    {
        int numOfScenePersist = FindObjectsOfType<ScenePersist>().Length;

        if (numOfScenePersist > 1)
        {

            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        // What is the current scene index?
        int curentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(curentSceneIndex != startSceneIndex)
        {
            Destroy(gameObject);
        }

    }
}
