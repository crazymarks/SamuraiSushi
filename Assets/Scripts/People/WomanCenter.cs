using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanCenter : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "MiddleZone")
        {
            this.transform.parent.GetComponent<Woman>().state_change();
        }
    }
}
