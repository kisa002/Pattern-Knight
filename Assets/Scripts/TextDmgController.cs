using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDmgController : MonoBehaviour
{
    Text text, childText;

    private void Awake()
    {
        text = GetComponent<Text>();
        childText = transform.GetChild(0).GetComponent<Text>();
    }

    public void StartAnimation()
    {
        StartCoroutine(CorAnimation());
    }

    IEnumerator CorAnimation()
    {
        childText.text = text.text;

        for (int i = 150; i > 100; i--)
        {
            text.color = new Color(0.7924528f, 0.07849769f, 0.7519448f, text.color.a - 0.02f);
            childText.color = new Color(1, 1, 1, childText.color.a - 0.02f);

            transform.Translate(Vector2.up * i * 0.02f);
            yield return new WaitForSeconds(.01f);
        }

        text.color = new Color(1, 1, 1, 1);
        childText.color = new Color(1, 1, 1, 1);

        transform.localPosition = new Vector2(20, -180);

        gameObject.SetActive(false);
    }
}
