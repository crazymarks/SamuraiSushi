using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour {
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.Instance.LoadScene(SceneManager.MainGame);
        }
	}
}
