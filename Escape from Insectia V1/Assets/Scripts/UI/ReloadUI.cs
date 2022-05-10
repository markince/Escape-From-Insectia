using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<GameSession>().ShowInventoryPanel();
        FindObjectOfType<GameSession>().ShowPlayerPanel();
        FindObjectOfType<GameSession>().ShowTimePanel();
        FindObjectOfType<GameSession>().ShowScorePanel();

    }

}
