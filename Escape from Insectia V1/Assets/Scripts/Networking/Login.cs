using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;



public class Login : MonoBehaviour
{
    public InputField userLoginInput;
    public InputField userPassword;
    public Text connectionResult;
    public GameObject loginPanel;
    public GameObject mainMenuPanel;


    [Serializable]
    public class UserDetails
    {
        public int id;
        public string username;
        public string password;
        public int    numofcoins;
        public int    numofgreengems;
        public int    numofbluegems;
        public int    numofredgems;

        public static UserDetails CreateFromJSON(string json)
        {
            return JsonUtility.FromJson<UserDetails>(json);
        }
    }

    [Serializable]
    public class LoginWrapper
    {
        public List<UserDetails> userData = new List<UserDetails>();
    }

    private const string URL = "https://vesta.uclan.ac.uk/~maince/PHPFiles/login.php";

    private const int MAX_LOGINDATA = 3;


    public void OnButtonClick()
    {
        // Get all the userdata from the server and store it in an array
        StartCoroutine(GetUserData(URL));
        //Debug.Log(userLoginInput.text);
    }

    // Send the request then wait here until it returns

    IEnumerator GetUserData(string url)
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

            try
            {

                LoginWrapper userData = JsonUtility.FromJson<LoginWrapper>(uwr.downloadHandler.text);

                // Extract the top 3 highscores
                int dataCount = 0;
                foreach (var record in userData.userData)
                {
                    if (dataCount < MAX_LOGINDATA)
                    {
                        dataCount += 1;
                        Debug.Log(record.id + ", " + record.username + ", " + record.password + ", " + record.numofcoins + ", " + record.numofgreengems + ", " + record.numofbluegems + ", " + record.numofredgems);

                        // Check if user exists on the server
                        if ((record.username == userLoginInput.text) && (record.password == userPassword.text))
                        {
                            // Show login status
                            StartCoroutine(LoginSucessfull());

                            // Update user information from the server
                            GameSession.username                  = record.username;
                            GameSession.password                  = record.password;
                            GameSession.accountCoinsCollected     = record.numofcoins;
                            GameSession.accountGreenGemsCollected = record.numofgreengems;
                            GameSession.accountBlueGemsCollected  = record.numofbluegems;
                            GameSession.accountRedGemsCollected   = record.numofredgems;

                            GameSession.alreadyLoggedIn = true;
                        }
                        else
                        {
                            StartCoroutine(LoginFailed());
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Debug.Log(ex.ToString());
            }
        }
    }

    IEnumerator LoginSucessfull()
    {
        connectionResult.text = ("Connecting...");

        yield return new WaitForSeconds(3);

        connectionResult.text = ("Login Successful!");

        yield return new WaitForSeconds(1.5f);

        loginPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    IEnumerator LoginFailed()
    {
        connectionResult.text = ("Connecting...");

        yield return new WaitForSeconds(3);

        connectionResult.text = ("Login Failed");

    }
}