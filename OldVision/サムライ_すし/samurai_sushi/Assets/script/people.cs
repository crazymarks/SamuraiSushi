using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class people : MonoBehaviour {
    private int enterdirection = 0;
    /*
     * 0=up 1=left 2=right
     */
    private float speed = 0f;
    public float maxspeed = 0f;
    public float minspeed = 0f;
    public Vector2 middlezone_leftup = new Vector2(-5.5f,-1.4f);
    public Vector2 middlezone_rightup = new Vector2(5.5f,-1.4f);
    public Vector2 middlezone_leftdown = new Vector2(-5.5f,-4.5f);
    public Vector2 middlezone_rightdown = new Vector2(5.5f, -4.5f);
    public float top_y = 2f;
    public float max_x = 18f;           //the range of left and right  x  position
    GameObject game_controller;
    

    public float p_ofcrowd = 0f;   //from 0 to  1   
    private Vector3 random_point = new Vector3(0, 0, 2);

    //about scale
    private float scale_rate = 0.0f;
    //about scale

	void Start ()
    {
        random_speed();
        game_controller = GameObject.Find("game_controller");
        scale_with_y();
    }
    void FixedUpdate()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, random_point, step);
        scale_with_y();


        if (this.transform.position.y >2||this.transform.position.x>17||this.transform.position.y<-17)   // exit screen delete    y can be changed
        {
            Destroy(gameObject);
        }

    }

    //enter
    void enter_fromup()
    {
        float x = 0f;
        float y = 0f;
        x = Random.Range(middlezone_leftup.x, middlezone_rightup.x);
        y = middlezone_rightup.y;
        random_point = new Vector3(x, y,2f);
        enterdirection = 0;
    }

    void enter_fromleft()
    {
        float x = 0f;
        float y = 0f;
        x = middlezone_leftup.x;
        y = Random.Range(middlezone_leftdown.y,middlezone_leftup.y);
        random_point = new Vector3(x, y,2f);
        enterdirection = 1;
    }

    void enter_fromright()
    {
        float x = 0f;
        float y = 0f;
        x = middlezone_rightup.x;
        y = Random.Range(middlezone_rightdown.y, middlezone_rightup.y);
        random_point = new Vector3(x, y,2f);
        enterdirection = 2;
    }
    //enter
    //exit
    void exit_toup()
    {
        float x = 0f;
        float y = 0f;
        x = Random.Range(middlezone_leftup.x+2, middlezone_rightup.x-2);
        y = top_y;
        random_point = new Vector3(x, y,2f);
    }
    void exit_toleft()
    {
        float x = 0f;
        float y = 0f;
        x = -(max_x);
        y = Random.Range(middlezone_leftdown.y, middlezone_leftup.y);
        random_point = new Vector3(x, y,2f);
    }
    void exit_toright()
    {
        float x = 0f;
        float y = 0f;
        x = max_x;
        y = Random.Range(middlezone_rightdown.y, middlezone_rightup.y);
        random_point = new Vector3(x, y,2f);
    }
    //exit

/// <summary>
/// enter crowd state 
/// sent message to game controller
/// </summary>
    void crowd()
    {
        game_controller.SendMessage("customers_crowd",this.gameObject.name); // send name to list
    }

    void change_state()
    {
        if (enterdirection == 0)                                //come from up
        {
            float nextdirection = Random.Range(0f,1f);
            if (nextdirection < p_ofcrowd)            // enter crowd
            {
                crowd();
            }else if(nextdirection<((1+p_ofcrowd)/2))    //go to right
            {
                exit_toright();
            }else                                     //go to left
            {
                exit_toleft();
            }

        }else if (enterdirection == 1)                           //come form left
        {
            float nextdirection = Random.Range(0f, 1f);
            if (nextdirection < p_ofcrowd)                // enter crowd
            {
                crowd();
            }
            else if (nextdirection < ((1 + p_ofcrowd) / 2))   //go to up
            {
                exit_toup();
            }
            else                                     //go to right
            {
                exit_toright();
            }

        }
        else                                                       //come form right
        {
            float nextdirection = Random.Range(0f, 1f);
            if (nextdirection < p_ofcrowd)                // enter crowd
            {
                crowd();
            }
            else if (nextdirection < ((1 + p_ofcrowd) / 2))   //go to up
            {
                exit_toup();
            }
            else                                     //go to left
            {
                exit_toleft();
            }
        }

    }
    
/// <summary>
/// after eat sushi  find a way to leave
/// </summary>
    void after_eatsushi()
    {
        Debug.Log("delicious!");
        if (enterdirection == 0)                                //come from up
        {
            float nextdirection = Random.Range(0f, 1f);
            if (nextdirection < 0.5f)            
            {
                exit_toright();
            }else                                     //go to left
            {
                exit_toleft();
            }

        }
        else if (enterdirection == 1)                           //come form left
        {
            float nextdirection = Random.Range(0f, 1f);
            if (nextdirection < 0.5f)                //go to up
            {
                exit_toup();                         
            }else                                     //go to right
            {
                exit_toright();
            }

        }else                                                       //come form right
        {
            float nextdirection = Random.Range(0f, 1f);
            if (nextdirection <0.5f)                //go to up
            {
                exit_toup();
            }else                                     //go to left
            {
                exit_toleft();
            }
        }
    }

    /// <summary>
    ///  be killed anime and 
    ///  sent message to game controller to delete the list of people
    /// </summary>
    void be_killed()
    {
        //play animation
        //play animation
        Destroy(gameObject);
        game_controller.SendMessage("kill_people",1);
    }

    // state change 
   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "middlezone")
        {
            change_state();
        }
        if (other.name == "Line")
        {
            be_killed();
        }
    }


    void random_speed()
    {
        speed=Random.Range(minspeed, maxspeed);
    }

    /// <summary>
    /// when moved. scale change with position y 
    /// </summary>
    void scale_with_y()       
    {
        if((this.transform.position.y) <-2)
        {
            scale_rate = this.transform.position.y * (-0.12f) + 0.22f;   //y=-0.12x +0.22
            this.transform.localScale = new Vector3(scale_rate, scale_rate, scale_rate);
        }
        if (transform.position.y >= -2)
        {
            scale_rate = this.transform.position.y * (-0.1f) + 0.25f;   //y=-0.1x +0.25
            this.transform.localScale = new Vector3(scale_rate, scale_rate, scale_rate);
        }
    }
}
