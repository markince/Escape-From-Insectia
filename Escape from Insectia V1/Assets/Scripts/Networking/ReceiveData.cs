using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ReceiveData : MonoBehaviour
{
    // Set access to scene high scores text objects
    public Text NameText1, ScoreText1;
    public Text NameText2, ScoreText2;
    public Text NameText3, ScoreText3;
    public Text NameText4, ScoreText4;
    public Text NameText5, ScoreText5;

    [Serializable]
    public class Player
    {
        public string name;
        public int score;

        public static Player CreateFromJSON(string json)
        {
            return JsonUtility.FromJson<Player>(json);
        }
    }

    [Serializable]
    public class ScoreWrapper
    {
        public List<Player> scoreTable;
    }

    private const string URL = "https://vesta.uclan.ac.uk/~maince/PHPFiles/scores.php";

    private const int MAX_HIGHSCORES = 5;

    void Start()
    {
        StartCoroutine(GetHighScoreData(URL));
    }

    // Send the request then wait here until it returns
    IEnumerator GetHighScoreData(string url)
    {
        var uwr = new UnityWebRequest(url, "GET");
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        // Send ther request, then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error while sending: " + uwr.error);
        }
        else
        {
            // Check JSON text coming back from server
            //Debug.Log(uwr.downloadHandler.text);

            try
            {
                ScoreWrapper highScores = JsonUtility.FromJson<ScoreWrapper>(uwr.downloadHandler.text);

                // Extract the top 3 highscores
                int dataCount = 0;
                foreach (var record in highScores.scoreTable)
                {
                    if (dataCount < MAX_HIGHSCORES)
                    {
                        dataCount += 1;
                        //Debug.Log(record.name + ", " + record.score);
                    }
                    else
                    {
                        break;
                    }
                }


                // Display name and score
                if (dataCount == MAX_HIGHSCORES)
                {
                    // 1st place
                    NameText1.text = highScores.scoreTable[0].name;
                    ScoreText1.text = highScores.scoreTable[0].score.ToString();

                    // 2nd place
                    NameText2.text = highScores.scoreTable[1].name;
                    ScoreText2.text = highScores.scoreTable[1].score.ToString();

                    // 3rd place
                    NameText3.text = highScores.scoreTable[2].name;
                    ScoreText3.text = highScores.scoreTable[2].score.ToString();

                    // 4th place
                    NameText4.text = highScores.scoreTable[3].name;
                    ScoreText4.text = highScores.scoreTable[3].score.ToString();

                    // 5th place
                    NameText5.text = highScores.scoreTable[4].name;
                    ScoreText5.text = highScores.scoreTable[4].score.ToString();

                }
            }
            catch (ArgumentException ex)
            {
                Debug.Log(ex.ToString());
            }
        }
    }
}
