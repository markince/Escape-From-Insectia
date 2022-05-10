using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverScoreText : MonoBehaviour
{
    [SerializeField] Text finalScoreText = null;


    private void Update()
    {
        finalScoreText.text = GameSession.playerScore.ToString();

    }




}
