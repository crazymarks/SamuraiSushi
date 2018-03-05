using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_type1 : MonoBehaviour {
    //direction flag
    private bool isleft = false;
    private bool isright = false;
    private bool isup   = false;
    private bool isdown  = false;
    private int cuttimes = 0;
    //direction flag

    //create and move
    private Rigidbody2D rb;
    private Vector2 force = new Vector2(0, 0);   //be changed by random_force();
    private float force_y;
    private float force_x;
    //create and move

    //cut check /about sushi
    public GameObject niku;
    public GameObject rice;
    public GameObject fish_type1_fail1;
    public GameObject fish_type1_fail2;
    public Vector3 CutPos = new Vector3(8, 1, -1);
    private Vector2 RicePos = new Vector2(0, 0);
    //cut check /about sushi

    //about controller
    GameObject game_controller;


    void Start ()
    {
        random_force();                        //a function to get random force
        rb=GetComponent<Rigidbody2D>();       
        rb.AddForce(force,ForceMode2D.Impulse);

        game_controller = GameObject.Find("game_controller");
	}
	

	void FixedUpdate () {

        if (cuttimes >= 2)     // up to 2 side be touched  
        {
            CutPos = this.transform.position;            //get rice and sushi and niku Location

            if (isleft == true && isright == true)       // succeed to cut fish
            {
                RicePos = new Vector2(this.transform.position.x , -3.1f);

                Instantiate(niku, CutPos, Quaternion.identity);
                Instantiate(rice, RicePos, Quaternion.identity);
                //add rice appear animation (uncompeleted)
                //add ricd appear animation 

                // add related to popular and point(uncompeleted)
                // add related to popular and point
            }else                                        //fail to cut fish 
            {
                Instantiate(fish_type1_fail1, CutPos, Quaternion.identity);
                Instantiate(fish_type1_fail2, CutPos, Quaternion.identity);
                // add related to popular and point(uncompeleted)
                // add related to popular and point
            }
            Destroy(gameObject);


        }

        if (this.transform.position.y < -4)   // exit screen delete    y can be changed
        {
            Destroy(gameObject);
        }

        
	}

    // direction flag
    void set_left(bool state)
    {
        isleft = state;
        cuttimes++;
    }
    void set_right(bool state)
    {
        isright = state;
        cuttimes++;
    }
    void set_up(bool state)
    {
        isup = state;
        cuttimes++;
    }
    void set_down(bool state)
    {
            cuttimes++;
        isdown = state;
    }

    //a function to get random force
    void random_force() 
    {
        force_x = Random.Range(1.5f, 9f);   //range of x 
        force_y = Random.Range(6f, 12f);   //range of y
        force =new Vector2(force_x, force_y);
    }
}
