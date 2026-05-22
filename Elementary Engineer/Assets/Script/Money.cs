using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    [Range(0, 99999999)]
    public int Value;

    public Image[] texts;
    public Sprite[] Money_SP;

    private void Start()
    {
        for (int i = 0; i < texts.Length; i++)
            texts[i].gameObject.SetActive(false);
    }

    private void Update()
    {
        int Value_Length = Value.ToString().Length;
        string Money_Value = Value.ToString("D9");

        for (int i = 0; i < Money_Value.Length; i++)
        {
            texts[i].sprite = Money_SP[int.Parse(Money_Value.Substring(i, 1))];
        }

        for (int i = 0; i < texts.Length; i++)
        {
            if (i >= texts.Length - Value_Length)
                texts[i].gameObject.SetActive(true);
            else
                texts[i].gameObject.SetActive(false);
        }
    }
}
