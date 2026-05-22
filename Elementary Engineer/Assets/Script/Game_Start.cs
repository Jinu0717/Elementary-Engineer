using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Start : MonoBehaviour
{
    public GameObject Starting;
    public bool Game_Set;
    public bool Click;

    public Image Curtain;
    public int Speed;
    public float Curtain_Y;

    public Image Alarm_Player;
    public int Player_Speed;
    public float Player_Y;

    public Image Title;
    public int Title_Speed;
    public float Title_Y;

    public Earth earth;

    public Image Alarm_Earth;
    public int Earth_Speed;
    public float Earth_X;

    public Image How_Screen;
    public bool On_How;

    private void Start()
    {
        Starting.SetActive(true);
        Game_Set = true;
        Curtain_Y = 0;
        Player_Anim();
        Title_Anim();
        Earth_Anim();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !Click && !On_How)
        {
            Click = true;
            earth.Message.SetActive(false);

            Curtain_Anim();
            Player_Anim();
            Title_Anim();
            Earth_Anim();
        }
    }

    public void Curtain_Anim()
    {
        Curtain_Y += Time.deltaTime * Speed;
        Curtain.rectTransform.anchoredPosition = new Vector3(-40, Curtain_Y, 0);

        if (Curtain_Y <= 1165.0f)
            Invoke("Curtain_Anim", 0.05f);
        else
        {
            Curtain.rectTransform.anchoredPosition = new Vector3(-40, 1165.0f, 0);
            Game_Set = false;
            earth.Initialization();
            Starting.SetActive(false);
        }
    }

    public void Player_Anim()
    {
        if (!Click)
        {
            Player_Y += Time.deltaTime * Player_Speed/4;
            Alarm_Player.rectTransform.anchoredPosition = new Vector3(-650, Player_Y, 0);

            if (Player_Y <= -650.0f)
                Invoke("Player_Anim", 0.05f);
            else
                Alarm_Player.rectTransform.anchoredPosition = new Vector3(-650, -650.0f, 0);
        }
        else
        {
            Player_Y -= Time.deltaTime * Player_Speed;
            Alarm_Player.rectTransform.anchoredPosition = new Vector3(-650, Player_Y, 0);

            if (Player_Y >= -1175.0f)
                Invoke("Player_Anim", 0.05f);
            else
                Alarm_Player.rectTransform.anchoredPosition = new Vector3(-650, -1175.0f, 0);
        }
    }

    public void Title_Anim()
    {
        if (!Click)
        {
            Title_Y -= Time.deltaTime * Title_Speed/3;
            Title.rectTransform.anchoredPosition = new Vector3(0, Title_Y, 0);

            if (Title_Y >= -460.0f)
                Invoke("Title_Anim", 0.05f);
            else
                Title.rectTransform.anchoredPosition = new Vector3(0, -460.0f, 0);
        }
        else
        {
            Title_Y += Time.deltaTime * Title_Speed;
            Title.rectTransform.anchoredPosition = new Vector3(0, Title_Y, 0);

            if (Player_Y <= 280.0f)
                Invoke("Title_Anim", 0.05f);
            else
                Title.rectTransform.anchoredPosition = new Vector3(0, 280.0f, 0);
        }
    }

    public void Earth_Anim()
    {
        if (!Click)
        {
            Earth_X -= Time.deltaTime * Earth_Speed/4;
            Alarm_Earth.rectTransform.anchoredPosition = new Vector3(Earth_X, -140, 0);

            if (Earth_X >= 15.0f)
                Invoke("Earth_Anim", 0.05f);
            else
                Alarm_Earth.rectTransform.anchoredPosition = new Vector3(15.0f, -140, 0);
        }
        else
        {
            Earth_X += Time.deltaTime * Earth_Speed;
            Alarm_Earth.rectTransform.anchoredPosition = new Vector3(Earth_X, -140, 0);

            if (Earth_X <= 300.0f)
                Invoke("Earth_Anim", 0.05f);
            else
                Alarm_Earth.rectTransform.anchoredPosition = new Vector3(300.0f, -140, 0);
        }
    }

    public void Click_Earth()
    {
        How_Screen.gameObject.SetActive(true);
        How_Screen.transform.parent.GetComponent<How>().Move_On = false;
        How_Screen.transform.parent.GetComponent<How>().How_Anim();
        On_How = true;
    }
}
