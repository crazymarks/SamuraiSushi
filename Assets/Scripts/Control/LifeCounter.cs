using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour {
    int lifeCount;
    public GameObject LifeImage;
    public GameObject youDied;

	void Start () {
        lifeCount = 6;      
	}
	
	void Update () {
        LifeImage.GetComponent<Image>().sprite = Resources.Load("NewUI/life_" + lifeCount.ToString(), typeof(Sprite)) as Sprite;
    }

    public void Damage()
    {
        lifeCount--;
        if(lifeCount <= 0)
        {
            GameObject.Find("GameController").GetComponent<GameController>().GameOver1(); 
        }
    }

    public void ComboHeal()
    {
        if (lifeCount <= 5)
        {
            lifeCount++;
        }
    }

}
