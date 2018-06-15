using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour {
    public List<GameObject> lifes;
    int lifeCount;

    public GameObject youDied;

	void Start () {
        lifeCount = transform.childCount;      
	}
	
	void Update () {
		
	}

    public void Damage()
    {
        lifeCount--;
        lifes[lifeCount].SetActive(false);
        if(lifeCount == 0)
        {
            GameObject.Find("GameController").GetComponent<GameController>().GameOver1(); 
        }
    }

}
