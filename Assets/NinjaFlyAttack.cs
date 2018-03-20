using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaFlyAttack : MonoBehaviour {
    private float Speed = 500.0f;
    private float ScaleRate = 1.0f;
	// Use this for initialization
	void Start () {
        Invoke("go_die",1.0f);
	}
	
	// Update is called once per frame
	void Update () {
        ScaleRate = ScaleRate + 0.1f;
        transform.Rotate(Vector3.forward * Time.deltaTime * Speed);
        transform.localScale = new Vector3(ScaleRate,ScaleRate, 1);
	}
    void go_die()
    {
        Destroy(this.gameObject);
    }
}
