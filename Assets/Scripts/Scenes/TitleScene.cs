using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour {

    public GameObject flashText;
    private float nextTime;
    private float interval = 0.5f;

    void Start()
    {
        nextTime = Time.time;
    }

    void Update () {

        if(Time.time > nextTime)
        {
            float textAlpha = flashText.GetComponent<CanvasRenderer>().GetAlpha();
            if(textAlpha == 1.0f)
            {
                flashText.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
            }
            else
            {
                flashText.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
            }
            nextTime += interval;
        }

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.Instance.LoadScene(SceneManager.MainGame);
        }
	}
}
