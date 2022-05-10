using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTutorial : MonoBehaviour
{
    [SerializeField] GameObject scorePanel;
    [SerializeField] GameObject clockPanel;
    [SerializeField] GameObject playerPanel;
    [SerializeField] GameObject inventoryPanel;

    void Start()
    {
        scorePanel.SetActive(false);
        clockPanel.SetActive(false);
        playerPanel.SetActive(false);
        inventoryPanel.SetActive(false);


        Time.timeScale = 0;

    }






}
