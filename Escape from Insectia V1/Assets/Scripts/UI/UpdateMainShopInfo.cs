using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateMainShopInfo : MonoBehaviour
{
    public Text accountCoinsAmountTextfield;
    public Text accountGreenGemsAmountTextfield;
    public Text accountBlueGemsAmountTextfield;
    public Text accountRedGemsAmountTextfield;




    // Update is called once per frame
    void Update()
    {
        accountCoinsAmountTextfield.text     = GameSession.accountCoinsCollected.ToString();
        accountGreenGemsAmountTextfield.text = GameSession.accountGreenGemsCollected.ToString();
        accountBlueGemsAmountTextfield.text  = GameSession.accountBlueGemsCollected.ToString();
        accountRedGemsAmountTextfield.text   = GameSession.accountRedGemsCollected.ToString();

    }
}
