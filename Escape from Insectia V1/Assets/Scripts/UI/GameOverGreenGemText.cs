using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverGreenGemText : MonoBehaviour
{
    [SerializeField] Text numOfFinalGreenGemText = null;


    private void Update()
    {
        numOfFinalGreenGemText.text = GameSession.numOfGreenGemsCollected.ToString();

    }
}
