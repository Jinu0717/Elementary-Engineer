using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Finish : MonoBehaviour
{
    public GameObject Finish;
    public bool Game_Set;

    public Image[] texts;
    public Sprite[] Money_SP;

    public Image Return_Button;
    public Sprite[] Return_SP;
    public int Button_Anim_Num;

    public Image Left_Title;
    public int Left_Speed;
    public float Left_Title_Y;

    public Image Right_Title;
    public int Right_Speed;
    public float Right_Title_Y;

    public Image Curtain;
    public int Speed;
    public float Curtain_Y;

    private void Start()
    {
        Button_Anim_Num = 0;
        Curtain_Y = 0;

        Left_Title_Anim();
        Right_Title_Anim();
    }

    private void Update()
    {
        if (!Game_Set) return;

        Finish.SetActive(true);

        Camera.main.orthographicSize = 5;
        Camera.main.transform.position = new Vector3(0, 0, -10);

        string Money_Value = FindObjectOfType<Money>().Value.ToString("D9");

        for (int i = 0; i < Money_Value.Length; i++)
        {
            texts[i].sprite = Money_SP[int.Parse(Money_Value.Substring(i, 1))];
        }
    }

    public void Button_Anim()
    {
        if (Button_Anim_Num < 5)
        {
            Return_Button.sprite = Return_SP[Button_Anim_Num];
            Button_Anim_Num++;

            Invoke("Button_Anim", 0.1f);
        }
        else
            Curtain_Anim();
    }

    public void Left_Title_Anim()
    {
        if(Finish.active)
            Left_Title_Y -= Time.deltaTime * Left_Speed;

        Left_Title.rectTransform.anchoredPosition = new Vector3(0, Left_Title_Y, 0);

        if (Left_Title_Y >= 280.0f)
            Invoke("Left_Title_Anim", 0.05f);
        else
        {
            Left_Title.rectTransform.anchoredPosition = new Vector3(0, 280.0f, 0);
        }
    }

    public void Right_Title_Anim()
    {
        if (Finish.active)
            Right_Title_Y -= Time.deltaTime * Left_Speed;

        Right_Title.rectTransform.anchoredPosition = new Vector3(0, Right_Title_Y, 0);

        if (Right_Title_Y >= 280.0f)
            Invoke("Right_Title_Anim", 0.05f);
        else
        {
            Right_Title.rectTransform.anchoredPosition = new Vector3(0, 280.0f, 0);
        }
    }

    public void Curtain_Anim()
    {
        Curtain_Y -= Time.deltaTime * Speed;
        Curtain.rectTransform.anchoredPosition = new Vector3(-40, Curtain_Y, 0);

        if (Curtain_Y >= -1080.0f)
            Invoke("Curtain_Anim", 0.05f);
        else
        {
            Curtain.rectTransform.anchoredPosition = new Vector3(-40, -1080.0f, 0);
            SceneManager.LoadScene("SampleScene");
        }
    }
}
