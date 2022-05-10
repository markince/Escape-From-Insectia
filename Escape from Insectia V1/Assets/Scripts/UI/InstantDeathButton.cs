using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeathButton : MonoBehaviour
{
    [SerializeField] public GameObject instantDeathPanel;
    public GameObject scorePanel;
    public GameObject clockPanel;
    public GameObject healthPanel;
    public GameObject inventoryPanel;

    public void IfClicked()
    {
        Player.showInstantDeathTutorial = true;
        Time.timeScale = 1.0f;
        instantDeathPanel.SetActive(false);
        FindObjectOfType<GameSession>().ShowScorePanel();
        FindObjectOfType<GameSession>().ShowPlayerPanel();
        FindObjectOfType<GameSession>().ShowTimePanel();
        FindObjectOfType<GameSession>().ShowInventoryPanel();


    }

}
