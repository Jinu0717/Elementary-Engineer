using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Earth : MonoBehaviour
{
    public int Elemental_Num;
    public int Flower_Count;

    public float Game_Timer;
    public int Min;
    public int Max;

    public GameObject Clicker;
    public GameObject Message;
    public GameObject shadow;
    public GameObject[] Elemental;

    public bool Close;
    public bool On_Scan;

    private void Start()
    {
        Elemental_Num = -1;

        Min = 0;
        Max = 1;

        for (int i = 0; i < 4; i++)
            Elemental[i].SetActive(false);

        Message.SetActive(false);
        On_Scan = false;
    }

    private void Update()
    {
        if (FindObjectOfType<Game_Finish>().Game_Set || FindObjectOfType<Game_Start>().Game_Set)
        {
            Clicker.SetActive(false);
            return;
        }

        Game_Timer += Time.deltaTime;

        if (Close && Message.active)
            Clicker.SetActive(true);
        else
            Clicker.SetActive(false);

        if (Elemental_Num >= 0)
        {
            if (Elemental[Elemental_Num].active && Close)
                Elemental[Elemental_Num].transform.GetChild(1).gameObject.SetActive(true);
            else
                Elemental[Elemental_Num].transform.GetChild(1).gameObject.SetActive(false);

            if (Elemental[Elemental_Num].GetComponent<Unit_Move>().Flower_Num <= 0)
            {
                Initialization();
            }
            else
                On_Scan = true;
        }

        shadow.SetActive(On_Scan);
    }

    public void Initialization()
    {
        Elemental_Num = -1;

        for (int i = 0; i < 4; i++)
            Elemental[i].SetActive(false);

        Message.SetActive(false);
        On_Scan = false;

        Invoke("Alarm", Random.Range(3, 7));
    }

    private void Leveling()
    {
        if (Game_Timer < 60) { Min = 1; Max = 2; }
        else if (Game_Timer < 120) { Min = 1; Max = 3; }
        else if (Game_Timer < 180) { Min = 1; Max = 4; }
        else if (Game_Timer < 240) { Min = 1; Max = 5; }
        else if (Game_Timer < 300) { Min = 2; Max = 3; }
        else if (Game_Timer < 360) { Min = 2; Max = 4; }
        else if (Game_Timer < 420) { Min = 2; Max = 5; }
        else if (Game_Timer < 480) { Min = 3; Max = 4; }
        else if (Game_Timer < 540) { Min = 3; Max = 5; }
        else { Min = 4; Max = 5; }
    }

    private void Alarm()
    {
        Leveling();
        Elemental_Num = Random.Range(0, 4);

        Flower_Count = Random.Range(Min, Max);
        Elemental[Elemental_Num].GetComponent<Unit_Move>().Flower_Num = Flower_Count;
        Message.SetActive(true);
        On_Scan = true;
    }

    public void click()
    {
        if (Close && Message.active)
        {
            Elemental[Elemental_Num].SetActive(true);
            Message.SetActive(false);
        }
    }
}
