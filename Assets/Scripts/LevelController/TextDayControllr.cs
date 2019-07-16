using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDayControllr : MonoBehaviour {
    public Image TextDay;
    public string daytext;
    public int day;
    public float TextDaya;
    public float flag;
    public bool flag2=false;//１２日目開始のフラグ
    public float flagtime;

    // Use this for initialization
    void Start () {
        GetComponent<Image>().material.color = new Color(1, 1, 1, 1);
        daytext = GameObject.Find("GameController").GetComponent<LevelReader>().DayControllerMytxt;
        day = int.Parse(daytext);
        if (day == 1||day==2)
        {
            flagtime = 1.0f;
        }
        else
        {
            flagtime = 3.0f;
            flag2 = true;
        }
        flag = 0;
        TextDay.GetComponent<Image>().sprite = Resources.Load("TextPic/textday"+ daytext, typeof(Sprite)) as Sprite;
        TextDaya = 1.0f;
    }
	

	// Update is called once per frame
	void Update () {
        flag += 0.01f;  
        if (TextDaya > 0&&flag>= flagtime&&flag2==true)
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
