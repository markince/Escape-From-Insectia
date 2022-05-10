using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSprite : MonoBehaviour
{

    private Material _mat;
    private Color[] _colors = { Color.yellow, Color.red };
    private float _flashSpeed = 0.1f;
    private float _lengthOfTimeToFlash = 1f;

    public void Awake()
    {

        this._mat = GetComponent<SpriteRenderer>().material;

    }
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Flash(this._lengthOfTimeToFlash, this._flashSpeed));
    }

    IEnumerator Flash(float time, float intervalTime)
    {
        float elapsedTime = 0f;
        int index = 0;
        while (elapsedTime < time)
        {
            _mat.color = _colors[index % 2];

            elapsedTime += Time.deltaTime;
            index++;
            yield return new WaitForSeconds(intervalTime);
        }
    }
}
