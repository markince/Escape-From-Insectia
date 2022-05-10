using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverBlueGemText : MonoBehaviour
{
    [SerializeField] Text numOfFinalBlueGemText = null;


    private void Update()
    {
        numOfFinalBlueGemText.text = GameSession.numOfBlueGemsCollected.ToString();

    }
}
