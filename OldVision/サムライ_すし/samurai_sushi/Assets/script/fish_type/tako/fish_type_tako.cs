using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_type_tako : MonoBehaviour {

    //direction flag
    private bool isup = false;
    private bool isleft = false;
    private bool isdown = false;
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
    public GameObject fish_type_tako_fail1;
    public GameObject fish_type_tako_fail2;
    private Vector3 CutPos = new Vector3(0, 0, 0);
    private Vector2 RicePos = new Vector2(0, 0);
    //cut check /about sushi

    //about controller
    GameObject game_controller;
    GameObject feet;
    private Vector2 force_feet = new Vector2(0, 0);
    private Rigidbody2D rb_f;
    //about controller
    
    void Start ()
    {
        random_force();                        //a function to get random force
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(force, ForceMode2D.Impulse);

        game_controller = GameObject.Find("game_controller");
    }
	
	void Update ()
    {
        if (this.transform.position.y < -4)   // exit screen delete    y can be changed
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Line")
        {

            if (isleft == true)
            {

                CutPos = this.transform.position;            //get rice and sushi and niku Location

                if (isup == true)       // Hand was Cutted
                {
                    Instantiate(fish_type_tako_fail1, CutPos, Quaternion.identity);
                    Destroy(this.gameObject);
                }

                if (isdown == true)       // Feet was Cutted
                {
                    for (int i = 0; i < 4; i++)
                    {
                        random_force_feet();
                        feet = Instantiate(fish_type_tako_fail2, CutPos, Quaternion.identity);
                        rb_f = feet.GetComponent<Rigidbody2D>();
                        rb_f.AddForce(force_feet, ForceMode2D.Impulse);
                    }
                }
                else
                {
                    RicePos = new Vector2(this.transform.position.x, -3.1f);

                    Instantiate(niku, CutPos, Quaternion.identity);
                    Instantiate(rice, RicePos, Quaternion.identity);
                }

                Destroy(this.gameObject);
            }
        }
    }

    void set_up(bool state)
    {
        isup = state;
    }
    void set_left(bool state)
    {
        isleft = state;
    }
    void set_down(bool state)
    {
        isdown = state;
    }
 
    /// <summary>
    /// a function to get random force for tako
    /// </summary>
    void random_force()
    {
        force_x = Random.Range(1.5f, 9f);   //range of x 
        force_y = Random.Range(6f, 12f);   //range of y
        force = new Vector2(force_x, force_y);
    }

    /// <summary>
    /// a function to get random force for tako's feet
    /// </summary>
    void random_force_feet()
    {
        force_x = Random.Range(-4f, 4f);   //range of x 
        force_y = Random.Range(-1f, 7f);   //range of y
        force_feet = new Vector2(force_x, force_y);
    }
}
