using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endOfLevelRedGemText : MonoBehaviour
{
    public Text redGemsText;

    private void Update()
    {
        redGemsText.text = GameSession.numOfRedGemsCollected.ToString();
    }
}
