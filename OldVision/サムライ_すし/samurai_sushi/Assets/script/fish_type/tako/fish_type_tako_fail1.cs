using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_type_tako_fail1 : MonoBehaviour {

    private Color color;
	// Use this for initialization
	void Start ()
    {
        Invoke("Destory_Itself", 10);
    }

    private void Update()
    {
        Color color = this.color;
        color.a -= 0.5f * Time.deltaTime;
        this.color = color;
    }

    // Update is called once per frame
    void Destory_Itself()
    {
        Destroy(this.gameObject);
    }

    
}
