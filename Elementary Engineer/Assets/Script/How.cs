using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class How : MonoBehaviour
{
    public GameObject[] Explanations;
    public GameObject LeftArrow;
    public GameObject RightArrow;

    public Game_Start game_Start;
    public float Pos_X;
    public int Speed;
    public bool Move_On;

    public int Explanation_Num; 

    private void Update()
    {
        if (Move_On)
        {
            if (Explanation_Num > 0 && Explanation_Num < 4)
            {
                LeftArrow.SetActive(true);
                RightArrow.SetActive(true);
            }
            else if (Explanation_Num >= 4)
                RightArrow.SetActive(false);
            else if (Explanation_Num <= 0)
                LeftArrow.SetActive(false);

            for (int i = 0; i < 5; i++)
            {
                if (i == Explanation_Num)
                    Explanations[i].SetActive(true);
                else
                    Explanations[i].SetActive(false);
            }
        }
    }

    public void Left_Arrow()
    {
        if (Move_On)
            Explanation_Num--;
    }

    public void Right_Arrow()
    {
        if (Move_On)
            Explanation_Num++;
    }

    public void How_Anim()
    {
        if (!Move_On)
        {
            Pos_X -= Time.deltaTime * Speed;
            game_Start.How_Screen.rectTransform.anchoredPosition = new Vector3(Pos_X, 0, 0);

            if (Pos_X >= -1920.0f)
                Invoke("How_Anim", 0.05f);
            else
            {
                game_Start.How_Screen.rectTransform.anchoredPosition = new Vector3(-1920.0f, 0, 0);
                Move_On = true;
            }
        }
        else
        {
            Pos_X += Time.deltaTime * Speed;
            game_Start.How_Screen.rectTransform.anchoredPosition = new Vector3(Pos_X, 0, 0);

            if (Pos_X <= 0.0f)
                Invoke("How_Anim", 0.05f);
            else
            {
                game_Start.How_Screen.rectTransform.anchoredPosition = new Vector3(0.0f, 0, 0);
                game_Start.On_How = false;
                game_Start.How_Screen.gameObject.SetActive(false);
            }
        }
    }

    public void Click()
    {
        if (Move_On)
            How_Anim();
    }
}
