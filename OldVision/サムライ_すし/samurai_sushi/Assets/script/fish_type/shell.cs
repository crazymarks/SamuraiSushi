using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell : MonoBehaviour {

    private bool isleft = false;
    private bool isright = false;
    private bool isup = false;
    private bool isdown = false;
    private int cuttimes = 0;
    //direction flag

    //create and move
    private Rigidbody2D rb;
    private Vector2 force = new Vector2(-3, 6);
    //create and move

    private GameObject parent;

    void Start()
    {
        parent = transform.parent.gameObject;
    }

    void FixedUpdate()
    {
        if (cuttimes >= 2)     // up to 2 side be touched  
        {
            Invoke("send_message", 0.1f);           

        }
        if (this.transform.position.y < -4)   // exit screen delete    y can be changed
        {
            Destroy(gameObject);
        }
    }

    // direction flag
    void set_left(bool state)
    {
        isleft = state;
        cuttimes++;
    }
    void set_right(bool state)
    {
        isright = state;
        cuttimes++;
    }
    void set_up(bool state)
    {
        isup = state;
        cuttimes++;
    }
    void set_down(bool state)
    {
        cuttimes++;
        isdown = state;
    }
    void send_message()
    {
        parent.SendMessage("shell_down");
         Destroy(this.gameObject);

    }
}
