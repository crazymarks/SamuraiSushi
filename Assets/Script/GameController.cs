using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    //people location
    private Vector2 PeopleCreatePoint;
    public float MaxY = 5f;        // screen`s max y  To define people`s create point 
    public float MaxX = 12f;       //screen`s max x   
    public float TopCreateVar = 4f; // define top create-zone
    public GameObject MiddleZone;

    //people location

    //about people
    public GameObject Man;           //一般人型 

    private GameObject People;          //the name of every human to manage them
    private int CreateDirection = 0;    //0=up 1=left 2=right
    private int PeopleCount = 0;        //give every people a number to make a list

    private float PeopleCreateSpeed; //the speed of people-creating
    private int PeopleKilling;       //the amount of killed people

    private List<string> CustomerList = new List<string>();
    private float PofCustomers = 0.8f;  // the probability of be a customer  range from 0 to 1 
    //about people

    //about Sushi
    //about Sushi


    //about point and eating sushi
    public Text MoneyText;
    public Text PeoplekillText; 
    private int Money = 0;            //dont forget to intialize it! 
    private bool CustomerFlag = false;

    //about point and eating sushi



    //about ninja
    //about ninja


    //about fish
    //about fish

 


//------------------------------fuction--------------------------------------------------------------------


	// Use this for initialization
	void Start () {
        create_people();
	}
	
	// Update is called once per frame
	void Update () {
        customers_list_check();
    }

    /// <summary>
    /// change state with different popular value
    /// </summary>
    void popular_check()
    {

    }

    /// <summary>
    /// create fish (define amount/type/point/etc.)
    /// </summary>
    void create_fish()
    {

    }

    /// <summary>
    ///  create people(amount/type/point/etc.)
    /// </summary>
    void create_people()
    {
        //addpeople choose(uncomplete)
        //addpeople choose(uncomplete)


        int i = Random.Range(0, 3);
        if (i == 0)      //create at up
        {
            float x = Random.Range(MiddleZone.transform.position.x - TopCreateVar, 
                MiddleZone.transform.position.x + TopCreateVar);
            PeopleCreatePoint = new Vector2(x, MaxY);
            People = Instantiate(Man, PeopleCreatePoint, Quaternion.identity);
            float j = Random.Range(0.0f, 1.0f); //compare to p of being a customer
            bool BeCustomer = false;
            if (j < PofCustomers)               //be a customer
            {
                BeCustomer = true;
                customers_listadd("People" + PeopleCount);   //add this name to customers list
            } 
                People.SendMessage("enter_from_up",BeCustomer);
            People.name = "People" + PeopleCount;
            PeopleCount++;            
        }
        else if (i == 1)   //create at left
        {
            float y = Random.Range(MiddleZone.transform.position.y-MiddleZone.GetComponent<BoxCollider2D>().size.y/2,
                MiddleZone.transform.position.y+ MiddleZone.GetComponent<BoxCollider2D>().size.y / 2);
            PeopleCreatePoint = new Vector2(-MaxX, y);
            People = Instantiate(Man, PeopleCreatePoint, Quaternion.identity);
            float j = Random.Range(0.0f, 1.0f); //compare to p of being a customer
            bool BeCustomer = false;
            if (j < PofCustomers)               //be a customer
            {
                BeCustomer = true;
                customers_listadd("People" + PeopleCount);    //add this name to customers list
            }
            People.SendMessage("enter_from_left",BeCustomer);
            People.name = "People" + PeopleCount;
            PeopleCount++;
        }
        else if(i == 2)  //create at right
        {
            float y = Random.Range(MiddleZone.transform.position.y - MiddleZone.GetComponent<BoxCollider2D>().size.y / 2,
                MiddleZone.transform.position.y + MiddleZone.GetComponent<BoxCollider2D>().size.y / 2);
            PeopleCreatePoint = new Vector2(MaxX, y);
            People = Instantiate(Man, PeopleCreatePoint, Quaternion.identity);
            float j = Random.Range(0.0f, 1.0f); //compare to p of being a customer
            bool BeCustomer = false;
            if (j < PofCustomers)               //be a customer
            {
                BeCustomer = true;
                customers_listadd("People" + PeopleCount);  //add this name to customers list
            }
                People.SendMessage("enter_from_right",BeCustomer);
            People.name = "People" + PeopleCount;
            PeopleCount++;
        }

        PeopleCreateSpeed = Random.Range(0.3f, 2f);       //speed of create speed 
        Invoke("create_people", PeopleCreateSpeed);
    }

    /// <summary>
    ///  to manage the list of waiting customer
    /// </summary>
    /// <param name="ObjectName"></param>
    void customers_listadd(string ObjectName)
    {
        CustomerList.Add(ObjectName);
        People.SendMessage("customers_check2", (CustomerList.Count - 1));
        Debug.Log("sent number");
        Debug.Log(CustomerList.Count - 1);
        Debug.Log(ObjectName);
    }
    /// <summary>
    /// to check the amount of customers now  
    /// change with popular
    /// </summary>
    void customers_list_check()
    {
        if (CustomerList.Count > 5)
        {
            PofCustomers = 0.0f;
        }
        else
        {
            PofCustomers = 0.8f;
        }
    }


    void kill_people(string name)
    {
        PeopleKilling = PeopleKilling + 1;

        CustomerList.Remove(name);
            customers_manage(1);    //only change number
       // PeoplekillText.text = ("殺人数" + PeopleKilling.ToString());
    }
    /// <summary>
    /// check after (eating sushi)(int i=0)/(kill people)(int i=1)/(add list)(int i=1)
    /// </summary>
    void customers_manage(int i)
    {
        if (i == 1)
        {
            for (int j = 0; j < CustomerList.Count; j++)
            {
                GameObject CustomersTemp = GameObject.Find(CustomerList[j]);
                CustomersTemp.SendMessage("customers_check2", j);
            }
        }
    }

}
