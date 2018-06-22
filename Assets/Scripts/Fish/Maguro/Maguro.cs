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

    // Update is called once per frame
    void FixedUpdate () {
        if (CutTimes >= 2)                      //up to 2 side be touched
        {
            CutPos = this.transform.position;             //get rice and sushi niku location

            if (IsLeft == true && IsRight == true)         //succeed to cut fish        
            {
                RicePos = new Vector3(this.transform.position.x, TableY,8);
                Instantiate(MaguroNiku, CutPos, Quaternion.identity);
                Instantiate(Rice, RicePos, Quaternion.identity);
                //add rice apper animation (uncompeleted)
                //add rice apper animation (uncompeleted)

                // add related to popular and point
                GameController.SendMessage("ComboCheck", "success");
                // add related to popular and point
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
