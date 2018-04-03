﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakoSumi : MonoBehaviour {

    private SpriteRenderer SR;
    private float Transparency =1.0f;
    private int count = 0;
	// Use this for initialization
	void Start () {
        SR = GetComponent<SpriteRenderer>();
        get_transparency();
    }

    void get_transparency()
    {
        count++;
        if (count > 50)
        {
            SR.color = new Vector4(SR.color.r, SR.color.g, SR.color.b, Transparency);
            Transparency -= 0.02f;
        }
        
        Invoke("get_transparency", 0.05f);
        if (Transparency <= 0f)
        {
            Destroy(gameObject);
        }
    }

}
