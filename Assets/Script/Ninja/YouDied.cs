using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouDied : MonoBehaviour {
    private SpriteRenderer SR;
    private float Transparency=0.0f;
	// Use this for initialization
	void Start () {
        SR = GetComponent<SpriteRenderer>();
        SR.color = new Vector4(SR.color.r, SR.color.g, SR.color.b, 0.0f);
        get_transparency();
	}
	
	// Update is called once per frame
	void get_transparency() {
        if(Transparency<=2.0f)
        {
            SR.color = new Vector4(SR.color.r, SR.color.g, SR.color.b, Transparency);
            Transparency += 0.05f;
            Invoke("get_transparency", 0.05f);
        }
        else
        {
            Time.timeScale = 0;
        }
	}
}
