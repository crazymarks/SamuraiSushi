using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maguro :FishBase {
    //direction flag
    private bool IsLeft = false;
    private bool IsRight = false;
    private bool IsUp = false;
    private bool IsDown = false;
    private int CutTimes = 0;
    //direction flag

    //cut check and sushi
    public GameObject MaguroNiku;
    public GameObject Rice;
    public GameObject MaguroFailedHead;
    public GameObject MaguroFailedTail;
    //9/16/1
    public GameObject Kira;
    //--------------------
    public Vector3 CutPos = new Vector3(0.0f, 0.0f, 7.0f);
    private Vector3 RicePos = new Vector3(0, 0, 0);
    private float TableY = -3.5f;  //the height of table
    //cut check and sushi
    
    //about controller
    GameObject GameController;

    protected override void Start()
    {
        base.Start();
        GameController = GameObject.Find("GameController");
    }

    //9/16/1　魚が切られたときのキラキラ表現
    //"9/16/1"で修正位置を確認してください
 
    int Kirasuu = 0;
    
    void Kirakira()
    {
        //！9/15!　きらきらが現れに回数。
        if (Kirasuu < 2)
        {
           // Debug.Log("position " + transform.position);
            //きらきらの位置
            Kira = Instantiate(Kira, new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, 1.0f), Quaternion.identity);

            Kirasuu++;
            Invoke("Kirakira", 0.2f);//毎回の待ち時間
        }
    }
    
    // Update is called once per frame
    void FixedUpdate () {
        
        if (CutTimes >= 2)                      //up to 2 side be touched
        {
            CutPos = this.transform.position;             //get rice and sushi niku location
            //
            if (IsLeft == true && IsRight == true)         //succeed to cut fish        
            {
                //9/16！
                Kirakira();
                RicePos = new Vector3(this.transform.position.x, TableY,8);
                Instantiate(MaguroNiku, CutPos, Quaternion.identity);
                //MaguroNiku = Instantiate(MaguroNiku, new Vector3(), Quaternion.identity);
              
                Instantiate(Rice, RicePos, Quaternion.identity);
                //add rice apper animation (uncompeleted)
                //add rice apper animation (uncompeleted)

                // add related to popular and point
                GameController.SendMessage("ComboCheck", "success");
                // add related to popular and point
             
                Debug.Log("kirakira");

                GameObject.Find("SEPlayer").GetComponent<PlaySE>().FishSuccess();    //SE再生
            }
            else                                               //fail to cut fish                 
            {
                Instantiate(MaguroFailedHead, CutPos, Quaternion.identity);
                Instantiate(MaguroFailedTail, CutPos, Quaternion.identity);
                //add related to popular and point (uncompeleted)
                GameController.SendMessage("ComboCheck", "fail");
                //add related to popular and point (uncompeleted)
                GameObject.Find("SEPlayer").GetComponent<PlaySE>().FishFail();    //SE再生
            }
            Destroy(gameObject);
        }
        if (this.transform.position.y < -4)   // exit screen delete    y can be changed
        {
            GameController.SendMessage("ComboCheck", "fail");
            Destroy(gameObject);
        }
    }

    //direction flag
    void set_left(bool state)
    {
        IsLeft = state;
        CutTimes++;
    }
    void set_right(bool state)
    {
        IsRight = state;
        CutTimes++;
    }
    void set_up(bool state)
    {
        IsUp = state;
        CutTimes++;
    }
    void set_down(bool state)
    {
        IsDown = state;
        CutTimes++;
    }
}
