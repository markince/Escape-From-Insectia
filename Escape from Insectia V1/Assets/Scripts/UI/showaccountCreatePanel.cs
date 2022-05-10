using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class showaccountCreatePanel : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject accountCreatePanel;

    public void OnButtonClick()
    {
        loginPanel.SetActive(false);
        accountCreatePanel.SetActive(true);
    }



}
