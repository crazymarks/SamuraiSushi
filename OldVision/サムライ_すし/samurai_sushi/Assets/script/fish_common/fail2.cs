using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fail2 : MonoBehaviour {

    private Rigidbody2D rb;
    private Vector2 force = new Vector2(3, 6);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 745) * Time.deltaTime);

        if (this.transform.position.y < -4)   // exit screen delete    y can be changed
        {
            Destroy(gameObject);
        }
    }
}
