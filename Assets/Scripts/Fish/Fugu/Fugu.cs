using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fugu : FishBase {
    //direction flag
    private bool IsUp = false;
    private bool IsDown = false;
    //direction flag

    //cut check and sushi
    public GameObject FuguNikuGolden;
    public GameObject FuguNikuPoison;
    public GameObject Rice;
    private Vector3 CutPos = new Vector3(0.0f, 0.0f, 7.0f);
    private Vector3 RicePos = new Vector3(0, 0, 0);
    private float TableY = -3.5f; //the height of table
    //cut check and sushi 

    GameObject GameController;

    protected override void Start()
    {
        base.Start();
        GameController = GameObject.Find("GameController");
    }

    // Update is called once per frame
    void FixedUpdate () {
        transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);

        if (this.transform.position.y<-4)  //exit screen delete
        {
            GameController.SendMessage("popular_fish_cut", "fail");
            Destroy(gameObject);
        }		
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Line")
        {
            if (IsUp == true)   //failed
            {
                RicePos = new Vector3(this.transform.position.x, TableY, 8);
                CutPos = this.transform.position; //get rice and niku location
                Instantiate(FuguNikuPoison, CutPos, Quaternion.identity);
                Instantiate(Rice, RicePos, Quaternion.identity);
                GameController.SendMessage("popular_fish_cut", "fail");
                Destroy(this.gameObject);
            }else if (IsDown == true)   //success
            {
                RicePos = new Vector3(this.transform.position.x, TableY, 8);
                CutPos = this.transform.position; //get rice and niku location
                Instantiate(FuguNikuGolden, CutPos, Quaternion.identity);
                Instantiate(Rice, RicePos, Quaternion.identity);
                GameController.SendMessage("popular_fish_cut", "success");
                Destroy(this.gameObject);
            }
        }
    }

    //direction flag
    void set_up(bool state)
    {
        IsUp = state;
    }
    void set_down(bool state)
    {
        IsDown = state;
    }
}
