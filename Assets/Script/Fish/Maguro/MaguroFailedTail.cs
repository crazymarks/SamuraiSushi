using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaguroFailedTail : MonoBehaviour {

    private Rigidbody2D Rigidbody;
    private Vector2 Force = new Vector2(3, 6);
    
    // Use this for initialization
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Rigidbody.AddForce(Force, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 745) * Time.deltaTime);

        if (this.transform.position.y < -4)  //exit screen delete
        {
            Destroy(gameObject);
        }
    }
}
