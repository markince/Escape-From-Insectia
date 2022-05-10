using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LevelLoadTimer = 2.0f;
    [SerializeField] float LevelExitSlowMotionAmount = 0.2f;
    [SerializeField] Player player;

    // Ui panels
    public GameObject scorePanel;
    public GameObject clockPanel;
    public GameObject healthPanel;
    public GameObject inventoryPanel;

    // has player touched the portal?
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.name == "Player")
        {
            StartCoroutine(LoadNextLevel());


        }
    }

    IEnumerator LoadNextLevel()
    {
        // Slow the level down
        Time.timeScale = LevelExitSlowMotionAmount;

        // Wait few seconds before moving to next level
        yield return new WaitForSecondsRealtime(LevelLoadTimer);

        player.transform.position = new Vector2(7.5f, 71.65f);

        // Return time back to normal
        Time.timeScale = 1.0f;

    }

}
