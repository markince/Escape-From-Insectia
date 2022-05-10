using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class alreadyLoggedInCheck : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject mainMenuPanel;


    // Start is called before the first frame update
    void Start()
    {
        if(GameSession.alreadyLoggedIn == true)
        {
            loginPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
    }

}
