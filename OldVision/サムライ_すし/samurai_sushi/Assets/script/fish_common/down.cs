using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class down : MonoBehaviour {

    private GameObject parent;

    void Start()
    {
        parent = transform.parent.gameObject;
    }

    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Line")
        {
            parent.SendMessage("set_down", true);
        }
    }
}
