using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FishBase : MonoBehaviour {
    //create and move
    protected Rigidbody2D Rigidbody;
    protected Vector2 Force = new Vector2(0, 0);   //be changed by random_force();
    protected float ForceY;
    protected float ForceX;
    //create and move

    //about controller
    GameObject GameController;

    // Use this for initialization
    protected virtual void Start ()
    {
        random_force();	  // function to get random force
        Rigidbody = GetComponent<Rigidbody2D>();
        Rigidbody.AddForce(Force, ForceMode2D.Impulse);

        GameController = GameObject.Find("GameController");
	}

    //a function to get random force
    protected virtual void random_force()
    {
        ForceX = Random.Range(2f, 12f);   //range of x 
        ForceY = Random.Range(10f, 11f);   //range of y
        Force = new Vector2(ForceX, ForceY);
    }
}
