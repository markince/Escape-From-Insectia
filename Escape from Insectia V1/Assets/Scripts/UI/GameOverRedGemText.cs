using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverRedGemText : MonoBehaviour
{
    [SerializeField] Text numOfFinalRedGemText = null;


    private void Update()
    {
        numOfFinalRedGemText.text = GameSession.numOfRedGemsCollected.ToString();

    }
}
