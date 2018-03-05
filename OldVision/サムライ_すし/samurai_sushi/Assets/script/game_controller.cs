using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game_controller : MonoBehaviour {
    //about location
    public float top_y = 1f;
    public float max_x = 12f;
    private Vector2 people_createpoint;
    public Vector2 middlezone_leftup = new Vector2(-5.5f, -1.4f);
    public Vector2 middlezone_rightup = new Vector2(5.5f, -1.4f);
    public Vector2 middlezone_leftdown = new Vector2(-5.5f, -4.5f);
    public Vector2 middlezone_rightdown = new Vector2(5.5f, -4.5f);
    //about location

    // about fish
    public GameObject fish_type1;         //list code 1
    public GameObject fish_type_crab;     //list code 2
    public GameObject fish_type_tako;    //list code 3
    public Vector3 fish_position = new Vector3(0f, -3f, 0f);
    public int createSpeed = 2;
    //about fish 

    //about people
    public GameObject people_normal;
    private int create_direction = 0;           // 0=up 1=left 2=right      
    private int people_count = 0;
    private GameObject people;
    private GameObject people_temp;
    private float people_createspeed;
    private int peoplekill_count=0;
    //about people

    //about ninja
    //about ninja

    //about customer list
    private List<string> customer_list = new List<string>();
    //about customer list

    //about sushi
    private int sushiname_count=0;
    private GameObject sushi;
    //about sushi


    //about winpoint and eat sushi
    public Text money_text;
    public Text peoplekill_count_text;
    private int money = 0;
    private int normal_price = 100;
    private int gold_price = 200;
    private int poison_price = 100;
    private List<int> sushi_list= new List<int>(); //save the types of sushi
/*    1 ====================normal
      2 ====================gold
      3 ====================poison       */
    public GameObject sushi_prefab;
    private GameObject sushi_delete;
    private int sushidelete_count=0;
    private bool customer_flag= true;      //be used to invoke the time of people move
    private float custom_movetime = 0.8f;
    private GameObject people_eatsushi;
    //about point and eat sushia

    // test
    private int count = 0;
    //test      

    void Start () {
        create_fish(); 
        create_people();
    }
	
	void Update () {
        if (customer_flag ==true)
        {
        eat_sushi();
        }

    }
    /// <summary>
    /// change state with popular value
    /// </summary>
    void popular_check()   //to check 
    {

    }
    /// <summary>
    /// create fish (amount type ) create_point 
    /// </summary>
    void create_fish()
    {
        //  Random.Range()

        //fish amount choose(uncomplete)(related to popular)
        //fish amount choose(uncomplete)(related to popular)

        //fish type choose (uncomplete)
        float probability_fish = 0;
        probability_fish = Random.Range(0.0f, 1.0f);
        if (probability_fish >= 0f && probability_fish < 0.6f)
        {
            Instantiate(fish_type1, fish_position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            Invoke("create_fish", createSpeed);
        }
        else if(probability_fish < 0.8f)
        {
            Instantiate(fish_type_crab, fish_position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            Invoke("create_fish", createSpeed);
        }else
        {
            Instantiate(fish_type_tako, fish_position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            Invoke("create_fish", createSpeed);
        }
        //fish type choose (uncomplete)
    }


    void create_people()
    {
        //addpeople choose(uncomplete)
        //addpeople choose(uncomplete)
        int i=Random.Range(0, 3);
        if (i == 0)              //create at up 
        {
            float x= Random.Range(middlezone_leftup.x + 2f, middlezone_rightup.x - 2f);
            people_createpoint = new Vector2(x, 2.0f);
            people =Instantiate(people_normal, people_createpoint, Quaternion.identity);
            people.name = "people"+people_count;
            people_temp = GameObject.Find("people" + people_count);  //sent the position 
            people_temp.SendMessage("enter_fromup");
            people_count++;
        }
        else if (i ==1)       //create at left
        {
            float y = Random.Range(middlezone_leftdown.y, middlezone_leftup.y);
            people_createpoint=new Vector2(-max_x,y);
            people = Instantiate(people_normal, people_createpoint, Quaternion.identity);
            people.name = "people" + people_count;
            people_temp = GameObject.Find("people" + people_count);  //sent the position 
            people_temp.SendMessage("enter_fromleft");
            people_count++;
        }
        else                 //create at right
        {
            float y = Random.Range(middlezone_leftdown.y, middlezone_leftup.y);
            people_createpoint = new Vector2(max_x, y);
            people = Instantiate(people_normal, people_createpoint, Quaternion.identity);
            people.name = "people" + people_count;
            people_temp = GameObject.Find("people" + people_count);  //sent the position 
            people_temp.SendMessage("enter_fromright");
            people_count++;
        }

        people_createspeed = Random.Range(0, 2f);                     //speed of creating
        Invoke("create_people",people_createspeed);
    }

    void customers_crowd(string objectname)       //the customers in the list
    {                                              //and add new customers to list
        customer_list.Add(objectname);
        Invoke("custom_flag_set", custom_movetime);

    }
    
    void create_ninja()
    {

    }

    void create_sushi(Vector2 position)
    {
        sushi=Instantiate(sushi_prefab, position, Quaternion.identity);
        sushi.name = "sushi" + sushiname_count;              //set new name
        sushiname_count++;        
        sushi_list.Add(1);       //add to sushi list    
        //test
        count++;
        //test
      
    }
    void eat_sushi()
    {
        if (sushi_list.Count>0 && customer_list.Count>0)  //customer exist and sushi exist
        {

            //add different point to different sushi
            switch (sushi_list[0])
            {
                case 1:
                    money = money + normal_price;
                    break;
                case 2:
                    money = money + gold_price;
                    break;
                case 3:
                    money = money + poison_price;
                    break;
            }
            money_text.text = money.ToString();

            sushi_list.RemoveAt(0);

            sushi_delete = GameObject.Find("sushi" + sushidelete_count);  // find next sushi to delete
            Debug.Log(sushi_delete.name);
            Destroy(sushi_delete);
            sushidelete_count++;

            //delete customer
            people_eatsushi = GameObject.Find(customer_list[0]);
            customer_list.RemoveAt(0);
            people_eatsushi.SendMessage("after_eatsushi");
            customer_flag = false;
            Invoke("customer_flag_set", custom_movetime);  
               
            //delete customer
       
            //add related to popular
            //add related to popular

            Debug.Log(money);
        }

    }
    void customer_flag_set()
    {
        customer_flag = true;
    }

    void kill_people(int x)
    {
        peoplekill_count= peoplekill_count + x;
        peoplekill_count_text.text = ("殺人数"+peoplekill_count.ToString());
    }

}
