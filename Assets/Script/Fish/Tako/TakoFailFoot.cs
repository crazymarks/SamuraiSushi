using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakoFailFoot : MonoBehaviour {

	void Update () {
        transform.Rotate(new Vector3(0, 0, 745) * Time.deltaTime);
        if(this.transform.transform.position.y<-4) //exit screen
        {
            Destroy(gameObject);
        }
		
	}
}
