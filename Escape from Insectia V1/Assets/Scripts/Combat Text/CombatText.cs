using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Scrolling combat text used for player hits in game
public class CombatText : MonoBehaviour
{
    private float speed;
    private Vector3 direction;
    private float fadeTime;

    void Update()
    {
        float translation = speed * Time.deltaTime;

        // Move text upwards
        transform.Translate(direction * translation);
    }

    public void Initialize(float speed, Vector3 direction, float fadeTime)
    {
        // Position text at start
        this.speed = speed;
        this.fadeTime = fadeTime;
        this.direction = direction;

        StartCoroutine(FadeOut());
    }


    private IEnumerator FadeOut()
    {
        // Fade out the text over time
        float startAlpha = GetComponent<Text>().color.a;

        // Used to progress the fade over time
        float rate = 1.0f / fadeTime;

        float progress = 0.0f;

        while (progress < 1.0f)
        {
            Color tmpColor = GetComponent<Text>().color; // Set colour

            GetComponent<Text>().color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, Mathf.Lerp(startAlpha, 0, progress));

            progress += rate * Time.deltaTime;

            yield return null;         // Return its progress all the time


        }

        Destroy(gameObject);
    }
}
