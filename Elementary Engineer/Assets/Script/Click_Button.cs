using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_Button : MonoBehaviour
{
    public Sprite[] Button_Sp;
    public int Sp_Num;

    private void Start()
    {
        Sp_Num = 0;
        InvokeRepeating("Anim", 0.0f, 0.1f);
    }

    void Anim()
    {
        if (FindObjectOfType<Game_Finish>().Game_Set || FindObjectOfType<Game_Start>().Game_Set) return;

        if (Sp_Num >= 4)
            Sp_Num = 0;

        GetComponent<SpriteRenderer>().sprite = Button_Sp[Sp_Num];
        Sp_Num++;
    }
}
