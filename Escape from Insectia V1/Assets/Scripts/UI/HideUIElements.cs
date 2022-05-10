using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideUIElements : MonoBehaviour
{


    private void Start()
    {
        FindObjectOfType<GameSession>().HideScorePanel();
        FindObjectOfType<GameSession>().HidePlayerPanel();
        FindObjectOfType<GameSession>().HideTimePanel();
        FindObjectOfType<GameSession>().HideInventoryPanel();


    }




}
