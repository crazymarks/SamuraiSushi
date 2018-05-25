﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerBody : MonoBehaviour {

    private SpriteRenderer SR;
    private float transparency = 1.0f;
    int speedCount = 0;
    float speed = 0.25f;
    Vector3 targetPosition;

    // Use this for initialization
    void Start()
    {
        targetPosition = this.transform.position + new Vector3(-0.15f, -0.2f, 0f);
        SR = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        speed = speedCount * -0.04f + 0.75f;
        if (speed < 0.25f)
        {
            speedCount = 12;
        }
        else { speedCount++; }
        float Step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Step);
        if (transform.position == targetPosition)
        {
            get_transparency();
        }
    }

    void get_transparency()
    {
        SR.color = new Vector4(SR.color.r, SR.color.g, SR.color.b, transparency);
        transparency -= 0.04f;

        Invoke("get_transparency", 0.05f);
        if (transparency <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
