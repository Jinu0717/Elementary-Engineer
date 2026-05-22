using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pot : MonoBehaviour
{
    public Camera camera;
    public Player_Move player;
    public GameObject flowers;
    public GameObject Clicker;
    public GameObject Block;
    public GameObject Seeds_Button;
    public GameObject plant_nutritional_supplement;

    public GameObject[] plant_nutritional_supplements;
    public GameObject[] Ground_Water;

    public GameObject[] Buttons;

    public int Pot_num;
    public int Tool_Num;

    public GameObject TV;

    public Image Plant_Bar;
    public Image Water_Bar;

    public float Speed;

    [Range(0, 100)]
    public float Plant_Gage;
    [Range(0, 100)]
    public float Water_Gage;

    public bool On_Flower;
    public bool On_Plant;
    public bool Close;

    private void Start()
    {
        player = FindObjectOfType<Player_Move>();
        camera = Camera.main;
    }

    private void Update()
    {
        if (FindObjectOfType<Game_Finish>().Game_Set || FindObjectOfType<Game_Start>().Game_Set)
        {
            plant_nutritional_supplements[1].SetActive(false);
            Ground_Water[1].SetActive(false);

            Clicker.SetActive(false);
            Seeds_Button.SetActive(false);

            return;
        }

        if (Close && !player.On_Mission)
            Clicker.SetActive(true);
        else
            Clicker.SetActive(false);

        Tool();
        Gage();

        if (On_Plant)
        {
            plant_nutritional_supplements[0].SetActive(false);
            plant_nutritional_supplement.SetActive(true);

            Plant_Gage += Time.deltaTime * Speed * 2.0f;

            if (Plant_Gage > 100.0f)
                Plant_Gage = 100.0f;

            plant_nutritional_supplement.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().fillAmount -= Time.deltaTime * 0.1f;

            if (plant_nutritional_supplement.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().fillAmount <= 0)
                On_Plant = false;
        }
        else
        {
            if(Tool_Num == 0)
                plant_nutritional_supplements[0].SetActive(true);
            plant_nutritional_supplement.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Block.SetActive(false);

            camera.orthographicSize = 5;
            camera.transform.position = new Vector3(0, 0, -10);
            player.On_Mission = false;

            plant_nutritional_supplements[1].transform.localPosition = new Vector3(0.5f, 0.15f, 0);
            plant_nutritional_supplements[1].SetActive(true);
            plant_nutritional_supplements[0].SetActive(false);

            Ground_Water[1].transform.localPosition = new Vector3(-0.5f, 0.4f, 0);
            Ground_Water[1].SetActive(true);
            Ground_Water[0].SetActive(false);

            if (Pot_num == 0)
                Seeds_Button.SetActive(false);

            Tool_Num = 0;
        }
    }

    public void Gage()
    {
        if (On_Flower)
        {
            Plant_Gage -= Time.deltaTime * Speed;
            Water_Gage -= Time.deltaTime * Speed;
        }
        else
            TV.SetActive(false);

        if (Plant_Gage <= 0)
        {
            Plant_Gage = 0;
            FindObjectOfType<Game_Finish>().Game_Set = true;
        }
        if (Water_Gage <= 0)
        {
            Water_Gage = 0;
            FindObjectOfType<Game_Finish>().Game_Set = true;
        }

        Plant_Bar.fillAmount = Plant_Gage/100.0f;
        Water_Bar.fillAmount = Water_Gage/100.0f;
    }

    public void Tool()
    {
        switch (Tool_Num)
        {
            case 0:
                plant_nutritional_supplements[1].SetActive(false);
                Ground_Water[1].SetActive(false);
                plant_nutritional_supplements[0].SetActive(true);
                Ground_Water[0].SetActive(true);

                Buttons[0].SetActive(false);
                Buttons[1].SetActive(false);
                break;
            case 1:
                plant_nutritional_supplements[1].SetActive(true);
                plant_nutritional_supplements[0].SetActive(false);
                Ground_Water[1].SetActive(false);
                Ground_Water[0].SetActive(true);

                plant_nutritional_supplements[1].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y, -Camera.main.transform.position.z));

                Buttons[1].SetActive(false);
                break;
            case 2:
                Ground_Water[1].SetActive(true);
                Ground_Water[0].SetActive(false);
                plant_nutritional_supplements[1].SetActive(false);
                plant_nutritional_supplements[0].SetActive(true);

                Ground_Water[1].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y, -Camera.main.transform.position.z));

                if (Ground_Water[1].transform.localPosition.x <= 0)
                    Ground_Water[1].GetComponent<SpriteRenderer>().flipX = true;
                else
                    Ground_Water[1].GetComponent<SpriteRenderer>().flipX = false;

                Buttons[0].SetActive(false);
                break;
        }
    }

    public void Clicked()
    {
        if(Close && !player.On_Mission)
        {
            Block.SetActive(true);
            camera.orthographicSize = 1;
            camera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
            player.On_Mission = true;

            if (Pot_num == 0)
                Seeds_Button.SetActive(true);
        }
    }

    public void Seed_Clicked(int num)
    {
        if (player.On_Mission && !On_Flower && Pot_num == 0)
        {
            Pot_num++;
            Seeds_Button.SetActive(false);
            flowers.SetActive(true);
            flowers.GetComponent<Flower>().Flower_Num = num;

            switch(num)
            {
                case 0:
                    flowers.GetComponent<Flower>().Elemental = "Fire";
                    break;
                case 1:
                    flowers.GetComponent<Flower>().Elemental = "Water";
                    break;
                case 2:
                    flowers.GetComponent<Flower>().Elemental = "Ground";
                    break;
                case 3:
                    flowers.GetComponent<Flower>().Elemental = "Air";
                    break;
            }

            Plant_Gage = Random.Range(50.0f, 100.0f);
            Water_Gage = Random.Range(50.0f, 100.0f);

            TV.SetActive(true);
            On_Flower = true;
        }
    }

    public void Tools_Clicked(int num)
    {
        if (Close && player.On_Mission && Pot_num == 1)
        {
            Tool_Num = num;
            Buttons[num - 1].SetActive(true);
        }
    }

    public void Flower_Manager(int num)
    {
        if(Tool_Num == num)
        {
            switch(Tool_Num)
            {
                case 1:
                    if (!On_Plant)
                    {
                        Tool_Num = 0;
                        plant_nutritional_supplement.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().fillAmount = 1;

                        On_Plant = true;
                    }

                    break;
                case 2:
                    Water_Gage += 10.0f;

                    if (Water_Gage > 100.0f)
                        Water_Gage = 100.0f;

                    break;
            }
        }
    }
}
