using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_type_tako_fail2 : MonoBehaviour {
    
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 745) * Time.deltaTime);

        if (this.transform.position.y < -4)   // exit screen delete    y can be changed
        {
            Destroy(gameObject);
        }
    }
}
