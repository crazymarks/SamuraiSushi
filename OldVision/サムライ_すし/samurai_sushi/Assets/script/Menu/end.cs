using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end : MonoBehaviour {

	public GameObject Ctrl;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Line")
		{
			Ctrl.SendMessage("End_Game");
        }

    }

}