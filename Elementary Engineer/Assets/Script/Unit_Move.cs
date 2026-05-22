using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Move : MonoBehaviour
{
    public SpriteRenderer Unit_SP;
    public Sprite[] Anim_Sprites;
    public SpriteRenderer Number;
    public Sprite[] Num_Sprite;

    public int Anim_Num;
    public int Flower_Num;
    public string Elemental;

    private void Start()
    {
        if (Random.Range(0, 2) == 0)
            InvokeRepeating("Anim", 0.0f, 0.1f);
    }

    private void Update()
    {
        if (FindObjectOfType<Game_Finish>().Game_Set || FindObjectOfType<Game_Start>().Game_Set) return;

        if (Flower_Num != 0)
            Number.sprite = Num_Sprite[Flower_Num - 1];
    }

    void Anim()
    {
        if (FindObjectOfType<Game_Finish>().Game_Set || FindObjectOfType<Game_Start>().Game_Set) return;

        if (Anim_Num >= 4)
            Anim_Num = 0;

        Unit_SP.sprite = Anim_Sprites[Anim_Num];
        Anim_Num++;
    }
}
