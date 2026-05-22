using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public Camera camera;
    public Sprite[] Water_SP;
    public SpriteRenderer Water_Sprite;
    public Player_Move player;
    public GameObject Clicker;
    public GameObject Hamer;
    public float Speed;
    public bool Close;

    [Range(0, 100)]
    public float Value;

    private void Start()
    {
        player = FindObjectOfType<Player_Move>();
        camera = Camera.main;
        Value = Random.Range(50.0f, 100.0f);
    }

    private void Update()
    {
        if (FindObjectOfType<Game_Finish>().Game_Set || FindObjectOfType<Game_Start>().Game_Set)
        {
            Hamer.SetActive(false);
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
            if (!player.On_Mission)
            {
                Clicker.SetActive(true);
                Hamer.SetActive(false);
            }
            else
            {
                Clicker.SetActive(false);

                Hamer.SetActive(true);
                Hamer.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y, -Camera.main.transform.position.z));
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.On_Mission = true;

                camera.orthographicSize = 2;
                camera.transform.position = new Vector3(-7.385f, -2.85f, -10);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                player.On_Mission = false;

                camera.orthographicSize = 5;
                camera.transform.position = new Vector3(0, 0, -10);
            }
        }
        else
        {
            Clicker.SetActive(false);
        }

        Water_Manage();
    }

    private void Water_Manage()
    {
        if (Value > 84)
            Water_Sprite.sprite = Water_SP[0];
        else if (Value > 70)
            Water_Sprite.sprite = Water_SP[1];
        else if (Value > 56)
            Water_Sprite.sprite = Water_SP[2];
        else if (Value > 42)
            Water_Sprite.sprite = Water_SP[3];
        else if (Value > 28)
            Water_Sprite.sprite = Water_SP[4];
        else if (Value > 14)
            Water_Sprite.sprite = Water_SP[5];
        else if (Value > 0)
            Water_Sprite.sprite = Water_SP[6];
        else
            Water_Sprite.sprite = Water_SP[7];
    }

    public void click()
    {
        if(Close)
        {
            Value += 10;

            if (Value > 100)
                Value = 100;
        }
    }
}
