using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 1.5f);
    }
}
