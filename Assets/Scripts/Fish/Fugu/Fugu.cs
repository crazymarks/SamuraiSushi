using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fugu : FishBase {
    //direction flag
    private bool IsUp = false;
    private bool IsDown = false;
    private bool cutIn = false;
    private bool cutOut = false;
    //direction flag

    //cut check and sushi
    public GameObject FuguNikuGolden;
    public GameObject FuguNikuPoison;
    public GameObject Rice;
    //9/16/1
    public GameObject Kira;
    //--------------------
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
        transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);

        if (this.transform.position.y<-4)  //exit screen delete
        {
            GameController.SendMessage("ComboCheck", "fail");
            Destroy(gameObject);
        }

        //切られた
        if (cutIn == true && cutOut == true)
        {
            if (IsUp == true)   //失敗
            {
                RicePos = new Vector3(this.transform.position.x, TableY, 8);
                CutPos = this.transform.position; //get rice and niku location
                Instantiate(FuguNikuPoison, CutPos, Quaternion.identity);
                Instantiate(Rice, RicePos, Quaternion.identity);
                GameController.SendMessage("ComboCheck", "success");
                Destroy(this.gameObject);
                GameObject.Find("SEPlayer").GetComponent<PlaySE>().FishSuccess();    //SE再生
            }
            else  if (IsDown == true)   //成功
            {
                //9/16/1
                Kirakira();
                RicePos = new Vector3(this.transform.position.x, TableY, 8);
                CutPos = this.transform.position; //get rice and niku location
                Instantiate(FuguNikuGolden, CutPos, Quaternion.identity);
                Instantiate(Rice, RicePos, Quaternion.identity);
                GameController.SendMessage("ComboCheck", "success");
                Destroy(this.gameObject);
                GameObject.Find("SEPlayer").GetComponent<PlaySE>().FishSuccess();    //SE再生
            }

        }      

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Line")
        {
            cutIn = true;         
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Line")
        {
            cutOut = true;
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
