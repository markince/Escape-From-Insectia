using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    // Global variables used throught the game 
    
    [SerializeField] public static int playerHealth              = 100;   // Player HPs
    [SerializeField] public static int playerScore               = 0;     // player score
    [SerializeField] public static int numOfCoinsCollected       = 0;     // Number of coins collected
    [SerializeField] public static int numOfGreenGemsCollected   = 0;     // Number of green gems collected
    [SerializeField] public static int numOfBlueGemsCollected    = 0;     // Number of blue gems collected
    [SerializeField] public static int numOfRedGemsCollected     = 0;     // Number of red gems collected
    [SerializeField] public static int accountCoinsCollected     = 0;     // Number of coins collected per account
    [SerializeField] public static int accountGreenGemsCollected = 0;     // Number of greeen gems collected per account
    [SerializeField] public static int accountBlueGemsCollected  = 0;     // Number of blue gems collected per account
    [SerializeField] public static int accountRedGemsCollected   = 0;     // Number of coins collected
    [SerializeField] public static bool playedTutorial           = false; // Helps reset the UI in the start screen 
    [SerializeField] public static string username;                       // Stores player username
    [SerializeField] public static string password;                       // Stores player password


    [SerializeField] Player player;              // main player
    [SerializeField] int numOfPlayerLives = 3;   // Number of lives in game


    [SerializeField] Text numOfHealthPoints  = null; // Text fields used to display data on screen
    [SerializeField] Text numOfLivesText     = null;
    [SerializeField] Text playerScoreText    = null;
    [SerializeField] Text numOfCoinsText     = null;
    [SerializeField] Text numOfBlueGemsText  = null;
    [SerializeField] Text numOfGreenGemsText = null;
    [SerializeField] Text numOfRedGemsText   = null;

    public static bool alreadyLoggedIn = false; // used to check if login should be displayed on game reset

    // Game objects used to hide various UI elements 
    public GameObject scorePanel     = null;
    public GameObject timePanel      = null;
    public GameObject playerPanel    = null;
    public GameObject inventoryPanel = null;


    private void Awake()
    {
        // Singleton pattern
        // We only want one of these objects to exsist at once
        // When we restart the scene, this particular instance will continue 
        // to exist

        // Reset all player data

        int numOfGameSessions = FindObjectsOfType<GameSession>().Length;

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
            Player.tutorialLevelWaypoint = 1;
            Player.showInstantDeathTutorial = false;

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

    // Add amount to player score
    public void AddToScore(int scoreAmountToAdd)
    {
        playerScore += scoreAmountToAdd;
        
        // Update text field on UI
        playerScoreText.text = playerScore.ToString();
    }

    // Increment player coins
    public void IncrementCoins(int scoreAmountToAdd)
    {
        // Update coins
        numOfCoinsCollected++;
        // Update text field on UI
        numOfCoinsText.text = numOfCoinsCollected.ToString();

        // Update score
        playerScore += scoreAmountToAdd;
        // Update text field on UI
        playerScoreText.text = playerScore.ToString();

    }

    // Increment green gems
    public void IncrementGreenGems(int scoreAmountToAdd)
    {
        // Update green gems
        numOfGreenGemsCollected++;
        // Update text field on UI
        numOfGreenGemsText.text = numOfGreenGemsCollected.ToString();
        // Update score
        playerScore += scoreAmountToAdd;
        // Update text field on UI
        playerScoreText.text = playerScore.ToString();
    }

    // Increment red gems
    public void IncrementRedGems(int scoreAmountToAdd)
    {
        // Update red gems
        numOfRedGemsCollected++;
        // Update text field on UI
        numOfRedGemsText.text = numOfRedGemsCollected.ToString();
        // Update score
        playerScore += scoreAmountToAdd;
        // Update text field on UI
        playerScoreText.text = playerScore.ToString();
    }

    // Increment blue gems

    public void IncrementBlueGems(int scoreAmountToAdd)
    {
        // Update blue gems
        numOfBlueGemsCollected++;
        // Update text field on UI
        numOfBlueGemsText.text = numOfBlueGemsCollected.ToString();
        // Update score
        playerScore += scoreAmountToAdd;
        // Update text field on UI
        playerScoreText.text = playerScore.ToString();

    }

    // Player injured function
    public void ProcessPlayerInjured(int injuredAmount)
    {
        // Reduce health
        playerHealth -= injuredAmount;

        // Update the health text on the UI
        numOfHealthPoints.text = playerHealth.ToString();

    }

    // Instant death (use when player touches rising water)
    public void ProcessPlayerInstantDeath()
    {
        // set health to 0
        playerHealth = 0;
        // Update the health text on the UI
        numOfHealthPoints.text = playerHealth.ToString();

    }


    // Reset player when dies
    public void ProcessPlayerDeath()
    {
        if (numOfPlayerLives > 1)
        {
            RemoveLife();
            numOfHealthPoints.text = playerHealth.ToString();
        }
        else // no lifes left?
        {
            ResetGameSession(); // reset game
        }
    }

    // Deduct a live from total
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

    // Resets the game session
    private void ResetGameSession()
    {
        // Load game over screen
        SceneManager.LoadScene(4);

        Destroy(gameObject);
    }


    // Getters used in other classes
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

    public void HideScorePanel()
    {
        scorePanel.SetActive(false);
    }

    public void HidePlayerPanel()
    {
        playerPanel.SetActive(false);
    }

    public void HideTimePanel()
    {
        timePanel.SetActive(false);
    }

    public void HideInventoryPanel()
    {
        inventoryPanel.SetActive(false);
    }

    public void ShowScorePanel()
    {
        scorePanel.SetActive(true);
    }

    public void ShowPlayerPanel()
    {
        playerPanel.SetActive(true);
    }

    public void ShowTimePanel()
    {
        timePanel.SetActive(true);
    }

    public void ShowInventoryPanel()
    {
        inventoryPanel.SetActive(true);
    }
}
