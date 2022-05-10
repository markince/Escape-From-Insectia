using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endOfLevelCoinsText : MonoBehaviour
{
    public Text coinsText;

    private void Update()
    {
        coinsText.text = GameSession.numOfCoinsCollected.ToString();
    }


}
