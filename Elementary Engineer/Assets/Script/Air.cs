using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : MonoBehaviour
{
    public Camera camera;
    public Sprite[] Air_SP;
    public SpriteRenderer Air_Sprite;
    public Sprite[] Pump_SP;
    public SpriteRenderer Pump_Sprite;
    public GameObject player;
    public GameObject Clicker;
    public float Speed;
    public int Pump_Num;
    public bool Close;

    [Range(0, 100)]
    public float Value;

    [Range(0, 255)]
    public byte Alpha_Color;

    private void Start()
    {
        player = FindObjectOfType<Player_Move>().gameObject;
        camera = Camera.main;
        Value = Random.Range(50.0f, 100.0f);
    }

    private void Update()
    {
        if (FindObjectOfType<Game_Finish>().Game_Set || FindObjectOfType<Game_Start>().Game_Set)
        {
            Clicker.SetActive(false);
            return;
        }

        Value -= Time.deltaTime * Speed;

        if (Value < 0)
        {
            Value = 0;
            FindObjectOfType<Game_Finish>().Game_Set = true;
        }

        if (Close)
        {
            if(!player.GetComponent<Player_Move>().On_Mission)
                Clicker.SetActive(true);
            else
                Clicker.SetActive(false);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.GetComponent<Player_Move>().On_Mission = true;
                player.GetComponent<Player_Move>().player_SP.color = new Color32(255, 255, 255, Alpha_Color);

                camera.orthographicSize = 3;
                camera.transform.position = new Vector3(2.45f, 2.5f, -10);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                player.GetComponent<Player_Move>().On_Mission = false;
                player.GetComponent<Player_Move>().player_SP.color = new Color32(255, 255, 255, 255);

                camera.orthographicSize = 5;
                camera.transform.position = new Vector3(0, 0, -10);
            }
        }
        else
            Clicker.SetActive(false);

        Air_Manage();
    }

    private void Air_Manage()
    {
        if (Value > 84)
            Air_Sprite.sprite = Air_SP[0];
        else if (Value > 72)
            Air_Sprite.sprite = Air_SP[1];
        else if (Value > 60)
            Air_Sprite.sprite = Air_SP[2];
        else if (Value > 48)
            Air_Sprite.sprite = Air_SP[3];
        else if (Value > 36)
            Air_Sprite.sprite = Air_SP[4];
        else if (Value > 24)
            Air_Sprite.sprite = Air_SP[5];
        else if (Value > 12)
            Air_Sprite.sprite = Air_SP[6];
        else if (Value > 0)
            Air_Sprite.sprite = Air_SP[7];
        else
            Air_Sprite.sprite = Air_SP[8];
    }

    public void Click()
    {
        if (Close && Pump_Num == 0)
        {
            Value += 10;

            if(Value > 100)
                Value = 100;

            Invoke("Anim", 0.0f);
        }
    }

    void Anim()
    {
        if (Pump_Num >= 5)
        {
            Pump_Num = 0;
        }
        else
        {
            Pump_Sprite.GetComponent<SpriteRenderer>().sprite = Pump_SP[Pump_Num];
            Pump_Num++;

            Invoke("Anim", 0.1f);
        }
    }
}
