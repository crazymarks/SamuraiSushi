using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaguroNiku : MonoBehaviour {

    private Rigidbody2D Rigidbody;
    private Vector2 Force = new Vector2(0, 6);

    private GameObject SushiNew;
    private Vector2 SushiPosition = new Vector2(0, 0);

    //about controller
    GameObject GameController;
    //！
    public GameObject Kira;
    int Kirasuu = 0;

    //

    // Use this for initialization
    void Start () {
        Rigidbody = GetComponent<Rigidbody2D>();
        Rigidbody.AddForce(Force, ForceMode2D.Impulse);
        //!鱼片出来的时候就出现
        //Kira = Instantiate(Kira, new Vector3(), Quaternion.identity);
        kira ();
        // Kira.transform.localPosition =  new Vector3(this.transform.position.x, this.transform.position.y + 0.1f, 5.0f);
        //
        GameController = GameObject.Find("GameController");
    }
    void kira() {
  
        if (Kirasuu< 3){

            Kira = Instantiate(Kira, new Vector3(this.transform.position.x, this.transform.position.y + 0.1f, 5.0f), Quaternion.identity);
            Kirasuu++;
          
            Invoke("kira", 0.2f);
        }
    }
    void Update()
    {　　// tuki
       
    }
        // Update is called once per frame
        void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Rice")
        {
            
            //delete niku and rice  create sushi 
            //send message to game control
           SushiPosition = new Vector2(this.transform.position.x, this.transform.position.y - 0.1f);
            //
           


            Destroy(collision.gameObject);
            Destroy(this.gameObject);

            //succeed to create new sushi
            GameController.SendMessage("create_magurosushi", SushiPosition);
        }
		
	}
}
