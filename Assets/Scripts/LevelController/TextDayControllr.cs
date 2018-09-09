using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDayControllr : MonoBehaviour {
    public Image TextDay;
    public string day;
    public float TextDaya;
    public float flag;
    // Use this for initialization
    void Start () {
        GetComponent<Image>().material.color = new Color(1, 1, 1, 1);
        flag = 0;
        TextDaya = 1.0f;
        day =GameObject.Find("GameController").GetComponent<LevelReader>().DayControllerMytxt;
        TextDay.GetComponent<Image>().sprite = Resources.Load("TextPic/textday"+day,typeof(Sprite)) as Sprite;
    }
	
	// Update is called once per frame
	void Update () {
        flag += 0.01f;
        if (TextDaya > 0&&flag>=3.0f)
        {
            TextDaya -= 0.01f;
            TextDay.GetComponent<Image>().color = new Color(1, 1, 1, TextDaya);
        }
    }
    public void Toumei()
    {
        GetComponent<Image>().material.color = new Color(1, 1, 1, 0);
    }
}
