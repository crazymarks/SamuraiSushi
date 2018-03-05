using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_type_crab : MonoBehaviour {

	//Create Flag
	private bool isleft = false;
	private bool isright = false;
	private bool isup   = false;
	private bool isdown  = false;
    //Now use cuttimes to check this.  
    private bool ShellCut  = false;
	private int cuttimes = 0;//If int can be -1.

	//Ctrl of Physix
	private Rigidbody2D rb;
	private Vector2 force = new Vector2(0, 0);
	private float force_y;
	private float force_x;

	public GameObject Fail_image1;                //
    public GameObject Fail_image2;
    public GameObject niku;                        //
	public GameObject rice;                        //
    public GameObject shell_fly;

	private Vector3 CutPos = new Vector3(0, 0, 0); //Postion of Cut place
	private Vector2 RicePos = new Vector2(0, 0);   //Postion of Rice

	//about controller
	GameObject game_controller;

	//Setting random force
	void Start ()
	{
		random_force();       //a function to get random force
		rb=GetComponent<Rigidbody2D>();
		rb.AddForce(force,ForceMode2D.Impulse);

		game_controller = GameObject.Find("game_controller");
	}

	//a function to get random force
	void random_force() 
	{
		force_x = Random.Range(1.5f, 9f);    //range of x 
		force_y = Random.Range(6f, 12f);     //range of y
		force =new Vector2(force_x, force_y);
	}
	//End of Setting random force

	void FixedUpdate () {

		if (ShellCut==true)    //Shell is cutted
		{
			if (cuttimes >= 2)                                                                  //Two place get cut
			{
				//If left wasn't get cut.
				if (isleft == false)                                                                //Left side didn't get cut
				{
					CutPos = new Vector2(this.transform.position.x - 1, this.transform.position.y);     //Get pos of cut(left).  //The X.pos should have test.
					RicePos = new Vector2(this.transform.position.x - 1, -3.1f);                        //And pos of rice(left) //The X.pos should have test.

					Instantiate(niku, CutPos, Quaternion.identity);                                     //Creat niku at CutPos
					Instantiate(rice, RicePos, Quaternion.identity);                                    //And creat rice at RicePos
				}
				//If left was cutted.
				else
				{
					Instantiate(Fail_image1, new Vector2(this.transform.position.x - 1, this.transform.position.y), Quaternion.identity);//The X.pos should have test.
				}

				//If right wasn't get cut.
				if (isright == false)                                                                //Right side didn't get cut
				{
					CutPos = new Vector2(this.transform.position.x + 1, this.transform.position.y);     //Get pos of cut(right)   //The X.pos should have test.
					RicePos = new Vector2(this.transform.position.x + 1, -3.1f);                        //And pos of rice(right) //The X.pos should have test.

					Instantiate(niku, CutPos, Quaternion.identity);                                     //Creat niku at CutPos
					Instantiate(rice, RicePos, Quaternion.identity);                                    //And creat rice at RicePos
				}
				//If right was cutted.
				else
				{
					Instantiate(Fail_image2, new Vector2(this.transform.position.x + 1, this.transform.position.y), Quaternion.identity);//The X.pos should have test.
				}

				Destroy(gameObject); //End of Cut
			}

		}//End of else of Shell cutted

		//Destory itself, if it's outside the window.
		if (this.transform.position.y < -4)   // exit screen delete    y can be changed
		{
			Destroy(gameObject);
		}

	}//End of FixedUpdate

	//Flag to check which side is been cutted.
	void set_left(bool state)
	{
		if(ShellCut == true)          //Use to check if shell was cut or not.
		{
			cuttimes++;
			isleft = state;
		}
	}
	void set_right(bool state)
	{
		if(ShellCut == true)          //Use to check if shell was cut or not.
		{
            cuttimes++;
            isright = state;
		}
	}
	void set_up(bool state)
	{
		if(ShellCut == true)          //Use to check if shell was cut or not.
		{
            cuttimes++;
            isup = state;
		}
	}
	void set_down(bool state)
	{
		if(ShellCut == true)          //Use to check if shell was cut or not.
		{
            cuttimes++;
            isdown = state;
		}
	}
	//End of Flag to check
    void shell_down()     //the shell has been cutted
    {
        rb.AddForce(new Vector2(-1f, 2f), ForceMode2D.Impulse);
        //force related to y (uncompeleted)
        ShellCut = true;
        Instantiate(shell_fly, this.transform.position, Quaternion.identity);
    }
} //End of MonoBehaviour

