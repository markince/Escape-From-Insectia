using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class backFromAccountScreen : MonoBehaviour
{
    public GameObject accountCreatePanel;
    public GameObject loginPanel;


    public void OnButtonClick()
    {
        accountCreatePanel.SetActive(false);
        loginPanel.SetActive(true);
    }



}
