using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endOfLevelGreenGemsText : MonoBehaviour
{
    public Text greenGemsText;

    private void Update()
    {
        greenGemsText.text = GameSession.numOfGreenGemsCollected.ToString();
    }
}
