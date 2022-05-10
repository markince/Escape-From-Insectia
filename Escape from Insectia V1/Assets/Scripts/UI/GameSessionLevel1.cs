using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSessionLevel1 : MonoBehaviour
{
    [SerializeField] public static int playerHealth = 100;
    [SerializeField] int numOfPlayerLives = 3;
    [SerializeField] public static int playerScore = 0;
    [SerializeField] public static int numOfCoinsCollected = 0;
    [SerializeField] public static int numOfGreenGemsCollected = 0;
    [SerializeField] public static int numOfBlueGemsCollected = 0;
    [SerializeField] public static int numOfRedGemsCollected = 0;
    [SerializeField] Player player;

    [SerializeField] Text numOfHealthPoints = null;
    [SerializeField] Text numOfLivesText = null;
    [SerializeField] Text playerScoreText = null;
    [SerializeField] Text numOfCoinsText = null;
    [SerializeField] Text numOfBlueGemsText = null;
    [SerializeField] Text numOfGreenGemsText = null;
    [SerializeField] Text numOfRedGemsText = null;


    private void Awake()
    {
        // Singleton pattern
        // We only want one of these objects to exsist at once
        // When we restart the scene, this particular instance will continue 
        // to exist

        // Reset all player data

        int numOfGameSessions = FindObjectsOfType<GameSessionLevel1>().Length;

        if (numOfGameSessions > 1)
        {
            gameObject.SetActive(false);



            Destroy(gameObject);
        }
        else
        {
            // Clear all data ready for a new game
            playerHealth = 100;
            numOfPlayerLives = 3;
            playerScore = 0;
            numOfCoinsCollected = 0;
            numOfGreenGemsCollected = 0;
            numOfBlueGemsCollected = 0;
            numOfRedGemsCollected = 0;

            DontDestroyOnLoad(gameObject);
        }


        // Set starter player health
        numOfHealthPoints.text = playerHealth.ToString();

    }


    void Start()
    {
        // Display number of lives
        numOfLivesText.text = numOfPlayerLives.ToString();

        // Display score
        playerScoreText.text = playerScore.ToString();

        // Reset Player Position
        player.transform.position = new Vector2(-8.5f, 3.0f);

    }

    public void AddToScore(int scoreAmountToAdd)
    {
        Debug.Log("Score: " + scoreAmountToAdd);

        playerScore += scoreAmountToAdd;




        //playerScore += 100;

        playerScoreText.text = playerScore.ToString();
    }

    public void IncrementCoins(int scoreAmountToAdd)
    {
        numOfCoinsCollected++;
        numOfCoinsText.text = numOfCoinsCollected.ToString();


        playerScore += scoreAmountToAdd;
        playerScoreText.text = playerScore.ToString();

    }

    public void IncrementGreenGems(int scoreAmountToAdd)
    {
        numOfGreenGemsCollected++;
        numOfGreenGemsText.text = numOfGreenGemsCollected.ToString();

        playerScore += scoreAmountToAdd;
        playerScoreText.text = playerScore.ToString();
    }

    public void IncrementRedGems(int scoreAmountToAdd)
    {
        numOfRedGemsCollected++;
        numOfRedGemsText.text = numOfRedGemsCollected.ToString();


        playerScore += scoreAmountToAdd;
        playerScoreText.text = playerScore.ToString();
    }


    public void IncrementBlueGems(int scoreAmountToAdd)
    {
        numOfBlueGemsCollected++;
        numOfBlueGemsText.text = numOfBlueGemsCollected.ToString();


        playerScore += scoreAmountToAdd;
        playerScoreText.text = playerScore.ToString();

    }

    public void ProcessPlayerInjured(int injuredAmount)
    {
        playerHealth -= injuredAmount;

        // Update the health text on the UI
        numOfHealthPoints.text = playerHealth.ToString();

    }

    public void ProcessPlayerInstantDeath()
    {
        playerHealth = 0;
        numOfHealthPoints.text = playerHealth.ToString();

    }


    public void ProcessPlayerDeath()
    {
        if (numOfPlayerLives > 1)
        {
            RemoveLife();
            numOfHealthPoints.text = playerHealth.ToString();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void RemoveLife()
    {
        numOfPlayerLives--; // Remove a life

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

        // Update the life text on the UI
        numOfLivesText.text = numOfPlayerLives.ToString();

        // Reset player health
        playerHealth = 100;
    }

    private void ResetGameSession()
    {

        SceneManager.LoadScene(3);

        Destroy(gameObject);
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }

    public void SetPlayerHealth(int amount)
    {
        playerHealth = amount;
    }

    public int GetNumCoinsCollected()
    {
        return numOfCoinsCollected;
    }

}

