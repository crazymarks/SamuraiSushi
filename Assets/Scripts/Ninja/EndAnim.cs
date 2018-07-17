using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnim : MonoBehaviour {

    private float timer;
    public float life;

	// Use this for initialization
	void Start () {        
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer > life)
        {
            Destroy(this.gameObject);
        }
    }
}
