using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tako : FishBase {
    //direction flag
    private bool IsUp = false;
    private bool IsLeft = false;
    private bool IsDown = false;
    private bool CutOut = false;
    //direction flag

    //cut check and sushi
    public GameObject TakoNiku;
    public GameObject Rice;
    public GameObject TakoFailFoot;
    public GameObject TakoSumi;
    private Vector3 CutPos = new Vector3(0.0f, 0.0f, 7.0f);
    private Vector3 RicePos = new Vector3(0, 0, 0);
    private float TableY = -3.5f; //the height of table 
    //cut check and sushi

    //about controller
    GameObject GameController;
    //create and move
    GameObject Feet;
    private Rigidbody2D RigidbodyFeet;
    private Vector2 ForceFeet = new Vector2(0, 0);
    private float ForceX1 = 0.0f;
    private float ForceY1 = 0.0f;

    protected override void Start()
    {
        base.Start();
        GameController = GameObject.Find("GameController");
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (this.transform.position.y < -4)    //exit screen delete
        {
            GameController.SendMessage("ComboCheck", "fail");
            Destroy(gameObject);
        }

        if (CutOut == true)
        {
            if (IsLeft == true)
            {
                CutPos = this.transform.position;      //get rice and sushi and niku Location
                Vector3 SumiPos = new Vector3(CutPos.x, CutPos.y, 0.5f);
                if (IsDown == true)    //Feet was Cutted
                {
                    for (int i = 0; i < 4; i++)
                    {
                        random_force_feet();
                        Feet = Instantiate(TakoFailFoot, CutPos, Quaternion.identity);
                        RigidbodyFeet = Feet.GetComponent<Rigidbody2D>();
                        RigidbodyFeet.AddForce(ForceFeet, ForceMode2D.Impulse);
                        GameController.SendMessage("ComboCheck", "fail");
                        GameObject.Find("SEPlayer").GetComponent<PlaySE>().FishFail();    //SE再生
                    }
                }
                else
                {
                    if (IsUp == true)     //Head was CUtted
                    {
                        Instantiate(TakoSumi, SumiPos, Quaternion.identity);                      
                    }
                    RicePos = new Vector3(this.transform.position.x, TableY, 8);

                    Instantiate(TakoNiku, CutPos, Quaternion.identity);
                    Instantiate(Rice, RicePos, Quaternion.identity);
                    //add rice appear animation (uncompeleted)
                    //add rice appear animation (uncompeleted)
                    GameController.SendMessage("ComboCheck", "success");
                    GameObject.Find("SEPlayer").GetComponent<PlaySE>().FishSuccess();    //SE再生
                }
                Destroy(this.gameObject);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "Line")
        {
            CutOut = true;
        }
    }

    /// <summary>
    /// a function to get random force for tako's feet 
    /// </summary>
    void random_force_feet()
    {
        ForceX1 = Random.Range(-4f, 4f);  //range of x 
        ForceY1 = Random.Range(-1f, 7f); //range of y
        ForceFeet = new Vector2(ForceX1, ForceY1);
    }

    //direction flag
    void set_up(bool state)
    {
        IsUp = state;
    }
    void set_left(bool state)
    {
        IsLeft = state;
    }
    void set_down(bool state)
    {
        IsDown = state;
    }
}
