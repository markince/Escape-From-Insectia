using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endOfLevelBlueGemText : MonoBehaviour
{
    public Text blueGemsText;

    private void Update()
    {
        blueGemsText.text = GameSession.numOfBlueGemsCollected.ToString();
    }
}
