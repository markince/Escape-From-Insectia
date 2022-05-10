using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCoinText : MonoBehaviour
{
    [SerializeField] Text numOfFinalCoinsText = null;


    private void Update()
    {
        numOfFinalCoinsText.text =  GameSession.numOfCoinsCollected.ToString();

    }

}
