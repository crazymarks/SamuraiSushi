using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    /*
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Line")
        {
            SceneManager.LoadScene(sceneToStart);
        }

    }*/

}
