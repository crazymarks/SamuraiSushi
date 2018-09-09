using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour {
    public float delax;
    public float time;
    public GameObject OpeningLeft;
    public GameObject OpeningRight;
	// Use this for initialization
	void Start () {

        Invoke("_Delete", 5.0f);
	}
	public void _Delete()
    {
        Destroy(this.gameObject);
    }
	// Update is called once per frame
	void Update () {
        time += 0.02f;
        if (time >= 1.0f)
        {
            OpeningLeft.transform.Translate(-delax, 0, 0);
            OpeningRight.transform.Translate(delax, 0, 0);
        }
    }
}
