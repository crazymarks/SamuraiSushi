using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PeopleBase : MonoBehaviour {
    protected int EnterDirection = 0;    // 0=up 1=left 2=right
    public float MaxY = 5f;        // screen`s max y  To define people`s create point 
    public float MaxX = 12f;       //screen`s max x   
    public float TopCreateVar = 4f; // define top create-zone
    protected float Speed = 0f;
    public float MaxSpeed = 10f;
    public float MinSpeed = 8f;

    GameObject GameController;
    GameObject MiddleZone;
    protected float MiddleZoneX;     //the position of middlezone
    protected float MiddleZoneY;
    protected float MiddleZoneWidth;
    protected float MiddleZoneHeight;

    protected bool IsCustomer = false;      //to know this is a customer or not
    protected int CustomerNumber = -1;       //the number of customer
    protected Vector2 RandomPoint = new Vector2(0,0);
    
    protected float ScaleRate = 0.0f;


    // Use this for initialization
    protected virtual void GetStart()
    {
        random_speed();
        scale_with_y();
        GameController = GameObject.Find("GameController");
        MiddleZone = GameObject.Find("MiddleZone");

        MiddleZoneWidth = MiddleZone.GetComponent<BoxCollider2D>().size.x;
        MiddleZoneHeight = MiddleZone.GetComponent<BoxCollider2D>().size.y;
        
        Vector3 Pos = MiddleZone.transform.position;
        MiddleZoneX = Pos.x;
        MiddleZoneY = Pos.y;
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate ()
    {
        float Step = Speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, RandomPoint, Step);
        scale_with_y();
	}


//--------------------------------enter----------------------------------------------
    protected virtual void enter_from_up(bool BeCustomer)
    {
        GetStart();
        float x = 0f;
        float y = 0f;
        x = Random.Range(MiddleZoneX - (MiddleZoneWidth /2),
            MiddleZoneX+(MiddleZoneWidth/2));           
        y = MiddleZoneY + MiddleZoneHeight / 2;
        RandomPoint = new Vector2(x, y);
        EnterDirection = 0; //from up
        IsCustomer = BeCustomer;   // this is a customer
    }

    protected virtual void enter_from_left(bool BeCustomer)
    {
        GetStart();
        float x = 0f;
        float y = 0f;
        x = MiddleZoneX - MiddleZoneWidth / 2;
        y = Random.Range(MiddleZoneY - MiddleZoneHeight / 2,
            MiddleZoneX + MiddleZoneHeight / 2);           
        RandomPoint = new Vector2(x, y);
        EnterDirection = 1; //from left
        IsCustomer = BeCustomer;   // this is a customer
    }

    protected virtual void enter_from_right(bool BeCustomer)
    {
        GetStart();
        float x = 0f;
        float y = 0f;
        x = MiddleZoneX + MiddleZoneWidth / 2;
        y = Random.Range(MiddleZoneY - MiddleZoneHeight / 2,
            MiddleZoneX + MiddleZoneHeight / 2);
        RandomPoint = new Vector2(x, y);
        EnterDirection = 2; //from left
        IsCustomer = BeCustomer;   // this is a customer
    }
//--------------------------------enter----------------------------------------------

//--------------------------------exit-----------------------------------------------    
    protected virtual void exit_to_up()
    {
        float x = 0f;
        float y = 0f;
        x = Random.Range(MiddleZoneX - TopCreateVar,
            MiddleZoneX + TopCreateVar);
        y = MaxY;
        RandomPoint = new Vector2(x, y);
    }
    protected virtual void exit_to_left()
    {
        float x = 0f;
        float y = 0f;
        x = -MaxX;
        y = Random.Range(MiddleZoneY - MiddleZoneHeight / 2,
            MiddleZoneX + MiddleZoneHeight / 2);
        RandomPoint = new Vector2(x, y);
    }
    protected virtual void exit_to_right()
    {
        float x = 0f;
        float y = 0f;
        x = MaxX;
        y = Random.Range(MiddleZoneY - MiddleZoneHeight / 2,
            MiddleZoneX + MiddleZoneHeight / 2);
        RandomPoint = new Vector2(x, y);
    }
    //--------------------------------exit-----------------------------------------------

    protected virtual void state_change()
    {
        if (IsCustomer)
        {
            customers_move(CustomerNumber);
        }else
        {
            if (EnterDirection == 0)                //come from up
            {
                float NextDirection = Random.Range(0.0f, 1.0f);
                if (NextDirection < 0.5f)
                {       exit_to_right(); }
                else
                {       exit_to_left();  }
            }
            else if (EnterDirection == 1)         //come from left
            {
                float NextDirection = Random.Range(0.0f, 1.0f);
                if (NextDirection < 0.5)
                {    exit_to_right();    }
                else
                {    exit_to_up();       }
            }
            else                                 //come from right
            {
                float NextDirection = Random.Range(0f, 1f);
                if (NextDirection < 0.5)
                {    exit_to_left();     }
                else
                {    exit_to_up();       }
            }
        }
    }

    /// <summary>
    ///  to move the position of customers in list
    /// </summary>
    protected virtual void customers_move(int number)
    {
        float y = number * 0.2f - 3;
       RandomPoint =new Vector2(0,y);
    }
    /// <summary>
    /// to change customers state (leave customers list)
    /// </summary>
    protected virtual void customers_check1(bool beCustomers)
    {
        IsCustomer = beCustomers;
        state_change();
    }
    protected virtual void customers_check2(int number)
    {
        CustomerNumber = number;
        state_change();
    }
    /// <summary>
    /// be killed anime and 
    /// sent message to game controller to delete the list of people
    /// </summary>
    protected virtual void be_killed()
    {
        //play animation
        //play animation
        GameController.SendMessage("kill_people", this.name);
        Destroy(gameObject);
    }

    //state change
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "MiddleZone")
        {
            state_change();
        }
        if (other.name == "Line")
        {
            if(this.transform.position.x != 0 || CustomerNumber ==0)   
                be_killed();
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "OutZone")
        {
            be_killed();
        }
    }

    protected virtual void random_speed()
    {
        Speed = Random.Range(MinSpeed, MaxSpeed);
    }

    protected virtual void scale_with_y()
    {
            ScaleRate = (this.transform.position.y-1) * (-0.06f)  +0.15f;    //y=-0.06x-0.85
            this.transform.localScale = new Vector3(ScaleRate, ScaleRate, 1);

            Vector3 Pos = this.transform.position;
            Pos.z = 2f * Pos.y + 17f;
            transform.position = Pos;
    }
}
