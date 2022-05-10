using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// manager class for scrolling combat text
public class CombatTextManager : MonoBehaviour
{
    // Variables
    private static CombatTextManager instance;
    public GameObject textPrefab;
    public RectTransform canvasTransform;
    public float speed;
    public float fadeTime;
    public Vector3 direction;

    // Creates an instance of the text manager
    public static CombatTextManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CombatTextManager>();
            }
            return instance;
        }
    }

    // Create the text to be displayed
    public void CreateText(Vector3 position, string text, Color color)
    {
        GameObject scrollingCombatText = Instantiate(textPrefab, position, Quaternion.identity);
        scrollingCombatText.transform.SetParent(canvasTransform);
        scrollingCombatText.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        scrollingCombatText.GetComponent<CombatText>().Initialize(speed, direction, fadeTime);
        scrollingCombatText.GetComponent<Text>().text = text;
        scrollingCombatText.GetComponent<Text>().color = color;
    }
 
}
