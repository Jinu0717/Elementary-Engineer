using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public GameObject Flowers;
    public GameObject Flower_Button;
    public Sprite[] Flower_SP;
    public Pot pot;
    public Earth unit;
    public Money money;
    public int Flower_Num;
    public string Elemental;

    [Range(0, 100)]
    public float Value;
    public float speed;

    private void Start()
    {
        Flower_Button.SetActive(false);
        unit = FindObjectOfType<Earth>();
        money = FindObjectOfType<Money>();
        Value = 0;
    }

    private void Update()
    {
        if (FindObjectOfType<Game_Finish>().Game_Set || FindObjectOfType<Game_Start>().Game_Set) return;

        Value += Time.deltaTime * speed;

        if (Value >= 100)
            Value = 100;

        Flower_Manage();
    }

    private void Flower_Manage()
    {
        if (Value >= 100)
        {
            GetComponent<SpriteRenderer>().sprite = Flower_SP[4 + Flower_Num * 5];
            Flower_Button.SetActive(true);
        }
        else if (Value > 75)
            GetComponent<SpriteRenderer>().sprite = Flower_SP[3 + Flower_Num * 5];
        else if (Value > 50)
            GetComponent<SpriteRenderer>().sprite = Flower_SP[2 + Flower_Num * 5];
        else if (Value > 25)
            GetComponent<SpriteRenderer>().sprite = Flower_SP[1 + Flower_Num * 5];
        else
            GetComponent<SpriteRenderer>().sprite = Flower_SP[0 + Flower_Num * 5];
    }

    public void Click()
    {
        if (unit.Elemental_Num >= 0 && unit.Elemental[unit.Elemental_Num].GetComponent<Unit_Move>().Elemental == Elemental)
        {
            unit.Elemental[unit.Elemental_Num].GetComponent<Unit_Move>().Flower_Num--;
            money.Value += Random.Range(unit.Min * 1000, unit.Max * 10000);
        }


        Value = 0;
        pot.On_Flower = false;
        pot.Pot_num = 0;
        pot.plant_nutritional_supplement.SetActive(false);
        pot.On_Plant = false;
        Flowers.SetActive(false);
    }
}
