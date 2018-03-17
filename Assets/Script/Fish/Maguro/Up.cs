using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up : MonoBehaviour {

    private GameObject Parent;

    // Use this for initialization
    void Start()
    {
        Parent = transform.parent.gameObject;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Line")
        {
            if (other.name == "Line")
            {
                Parent.SendMessage("set_up", true);
            }
        }
    }
}
