using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public Vector2 inputVec;
    public Rigidbody2D rigid;
    public SpriteRenderer player_SP;
    public Sprite[] Anim_Sprites;

    public int Speed;
    public int Anim_Num;
    public bool Walk;
    public bool On_Mission;

    private void Start()
    {
        InvokeRepeating("Anim", 0.0f, 0.1f);
    }

    private void Update()
    {
        if (FindObjectOfType<Game_Finish>().Game_Set || FindObjectOfType<Game_Start>().Game_Set) return;

        if (!On_Mission)
        {
            inputVec.x = Input.GetAxisRaw("Horizontal");
            inputVec.y = Input.GetAxisRaw("Vertical");
        }
        else
            inputVec = new Vector2(0, 0);

        if (inputVec == new Vector2(0, 0))
            Walk = false;
        else
            Walk = true;

        if (inputVec.x == -1)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (inputVec.x == 1)
            transform.localScale = new Vector3(1, 1, 1);
    }

    private void FixedUpdate()
    {
        if (FindObjectOfType<Game_Finish>().Game_Set || FindObjectOfType<Game_Start>().Game_Set) return;

        Vector2 nextVec = inputVec.normalized * Speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);

        player_SP.GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.y;
    }

    void Anim()
    {
        if (FindObjectOfType<Game_Finish>().Game_Set || FindObjectOfType<Game_Start>().Game_Set) return;

        if (Anim_Num >= 4)
            Anim_Num = 0;

        if (Walk)
        {
            player_SP.GetComponent<SpriteRenderer>().sprite = Anim_Sprites[Anim_Num];
            Anim_Num++;
        }
        else
        {
            if(Anim_Num == 1)
                Anim_Num = 2;
            else if (Anim_Num == 3)
                Anim_Num = 0;

            player_SP.GetComponent<SpriteRenderer>().sprite = Anim_Sprites[Anim_Num];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Table")
            collision.gameObject.transform.GetChild(2).GetComponent<Pot>().Close = true;
        if (collision.tag == "Fire")
            collision.GetComponent<Fire>().Close = true;
        if (collision.tag == "Air")
            collision.GetComponent<Air>().Close = true;
        if (collision.tag == "Water")
            collision.GetComponent<Water>().Close = true;
        if (collision.tag == "Unit")
            collision.GetComponent<Earth>().Close = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Table")
            collision.gameObject.transform.GetChild(2).GetComponent<Pot>().Close = false;
        if (collision.tag == "Fire")
            collision.GetComponent<Fire>().Close = false;
        if (collision.tag == "Air")
            collision.GetComponent<Air>().Close = false;
        if (collision.tag == "Water")
            collision.GetComponent<Water>().Close = false;
        if (collision.tag == "Unit")
            collision.GetComponent<Earth>().Close = false;
    }
}
