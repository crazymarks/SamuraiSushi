using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouDied : MonoBehaviour {
    private SpriteRenderer SR;
    private float Transparency=0.0f;
	void Start () {
        SR = GetComponent<SpriteRenderer>();
        SR.color = new Vector4(SR.color.r, SR.color.g, SR.color.b, 0.0f);
        get_transparency();
	}
	//「you　die」がだんだん透明化して、最後ゲームが止まる
	void get_transparency() {
        if(Transparency<=2.0f)
        {            
            SR.color = new Vector4(SR.color.r, SR.color.g, SR.color.b, Transparency);
            Transparency += 0.025f;
            
            Invoke("get_transparency", 0.01f);
        }
        else
        {
            Time.timeScale = 0;  //ゲームを止める
        }
	}
}
