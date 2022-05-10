using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOfLevelCoinAmountText : MonoBehaviour
{
    [SerializeField] Text numOfCoinsText = null;


    private void Update()
    {
        numOfCoinsText.text = GameSession.numOfCoinsCollected.ToString();

    }


}
