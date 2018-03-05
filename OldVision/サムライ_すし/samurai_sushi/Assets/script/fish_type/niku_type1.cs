using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class niku_type1 : MonoBehaviour {

    private Rigidbody2D rb;
    private Vector2 force = new Vector2(0, 6);
    
    private GameObject sushi_new;     //to change new sushi name
    private int num_sushi=0;            //the number of created sushi
    private Vector2 sushi_position = new Vector2(0, 0);

    //about controller
    GameObject game_controller;
    //about controller

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(force, ForceMode2D.Impulse);

        game_controller = GameObject.Find("game_controller");
    }
   
   void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name== "rice(Clone)")
        {
            //delete niku and rice      create sushi  need to turn this function to game control
            sushi_position = new Vector2(this.transform.position.x, this.transform.position.y - 0.1f);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);

            //succeed to create new sushi
            game_controller.SendMessage("create_sushi",sushi_position);


        }
    }
}
