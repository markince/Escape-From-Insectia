using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class createAccount : MonoBehaviour
{
    public Text usernameInput;
    public Text passwordInput;
    public Text passwordInput2;
    public Text infoTextField;

    public Text JsonData;
    public Text ResultData;

    public GameObject createAccountPanel;
    public GameObject mainMenuPanel;


    [System.Serializable]
    public class DataToSend
    {
        public string username;
        public string password;
        public int    numofcoins;
        public int    numofgreengems;
        public int    numofbluegems;
        public int    numofredgems;
    }


    private string       URL = "https://vesta.uclan.ac.uk/~maince/PHPFiles/createaccount.php";



    // Start is called before the first frame update
    public void OnButtonClick()
    {

        // check user has type password correctly twice
        if (passwordInput.text != passwordInput2.text)
        {
            infoTextField.text = "Password does not match!";
        }
        else
        {
            // Send new data to server
            DataToSend myData = new DataToSend();

            myData.username = usernameInput.text;
            myData.password = passwordInput.text;
            myData.numofcoins = 0;
            myData.numofgreengems = 0;
            myData.numofbluegems = 0;
            myData.numofredgems = 0;

            string jsonData = JsonUtility.ToJson(myData);
            JsonData.text = jsonData;

            StartCoroutine(PostRequestJSON(URL, jsonData));

            // Update player data
            GameSession.username                  = myData.username;
            GameSession.password                  = myData.password;
            GameSession.accountCoinsCollected     = myData.numofcoins;
            GameSession.accountGreenGemsCollected = myData.numofgreengems;
            GameSession.accountBlueGemsCollected  = myData.numofbluegems;
            GameSession.accountRedGemsCollected   = myData.numofredgems;

            // Display sucess to player and close window
            StartCoroutine(ConfirmAccountCreation());

            
        }

    }


    IEnumerator PostRequestJSON(string url, string json)
    {
        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        // Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error while sending: " + uwr.error);
            ResultData.text = uwr.error;
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            ResultData.text = uwr.downloadHandler.text;
        }
    }


    IEnumerator ConfirmAccountCreation()
    {
        infoTextField.text = ("Account created sucessfully");

        yield return new WaitForSeconds(3);

        createAccountPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }



}