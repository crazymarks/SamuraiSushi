using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuguNikuPoison : MonoBehaviour {
    private Rigidbody2D Rigidbody;
    private Vector2 Force = new Vector2(0, 6);

    private GameObject SushiNew;
    private Vector2 SushiPosition = new Vector2(0, 0);

    //about controller
    GameObject GameController;

    // Use this for initialization
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Rigidbody.AddForce(Force, ForceMode2D.Impulse);

        GameController = GameObject.Find("GameController");
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag== "Rice")
        {
            //delete niku and rice  create sushi 
            //send message to game control
            SushiPosition = new Vector2(this.transform.position.x, this.transform.position.y - 0.1f);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);

            //succeed to create new sushi
            GameController.SendMessage("create_fugusushipoison", SushiPosition);
        }
    }
}
