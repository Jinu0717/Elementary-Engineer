using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public Camera camera;
    public Sprite[] Fire_SP;
    public GameObject Clicker;
    public GameObject player;
    public GameObject material;
    public float Speed;
    public bool Close;
    public bool Catch;

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
            material.SetActive(false);
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
            if (!player.GetComponent<Player_Move>().On_Mission)
                Clicker.SetActive(true);
            else
                Clicker.SetActive(false);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.GetComponent<Player_Move>().On_Mission = true;
                player.GetComponent<Player_Move>().player_SP.color = new Color32(255, 255, 255, Alpha_Color);

                camera.orthographicSize = 2.5f;
                camera.transform.position = new Vector3(-5.5f, 2.25f, -10);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                player.GetComponent<Player_Move>().On_Mission = false;
                player.GetComponent<Player_Move>().player_SP.color = new Color32(255, 255, 255, 255);

                Catch = false;
                material.SetActive(false);

                camera.orthographicSize = 5;
                camera.transform.position = new Vector3(0, 0, -10);
            }
        }
        else
            Clicker.SetActive(false);

        Fire_Manage();
    }

    private void Fire_Manage()
    {
        if (Value > 84)
            GetComponent<SpriteRenderer>().sprite = Fire_SP[0];
        else if (Value > 70)
            GetComponent<SpriteRenderer>().sprite = Fire_SP[1];
        else if (Value > 56)
            GetComponent<SpriteRenderer>().sprite = Fire_SP[2];
        else if (Value > 42)
            GetComponent<SpriteRenderer>().sprite = Fire_SP[3];
        else if (Value > 28)
            GetComponent<SpriteRenderer>().sprite = Fire_SP[4];
        else if (Value > 14)
            GetComponent<SpriteRenderer>().sprite = Fire_SP[5];
        else if (Value > 0)
            GetComponent<SpriteRenderer>().sprite = Fire_SP[6];
        else
            GetComponent<SpriteRenderer>().sprite = Fire_SP[7];
    }

    public void Tree_Click()
    {
        if (Close && !Catch && player.GetComponent<Player_Move>().On_Mission)
        {
            Catch = true;
            material.SetActive(true);

            Invoke("Tree", 0.0f);
        }
    }

    public void Fire_Click()
    {
        if (Close && Catch && player.GetComponent<Player_Move>().On_Mission)
        {
            Catch = false;
            material.SetActive(false);

            Value += 10;

            if (Value > 100)
                Value = 100;
        }
    }

    private void Tree()
    {
        material.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));

        if(Catch)
            Invoke("Tree", 0.1f);
    }
}
