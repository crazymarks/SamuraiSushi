using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouDied : MonoBehaviour
{
    private SpriteRenderer SR;
    
    //1
    private float step1;
    private float step2;
    private float step3;
    float speed = 60;
    float speed2 = 20;
    float speed3 = 30;
    public GameObject DoorRight;
    public GameObject DoorLeft;
    int DoorState ;
    //2
    void Start()
    {
        DoorState = 0;
        step1 = speed * Time.deltaTime;
        step2 = speed2 * Time.deltaTime;
        step3 = speed3 * Time.deltaTime;
        DoorLeft = Instantiate(DoorLeft, new Vector3(-12.0f, 0.0f, 0.5f), Quaternion.identity);
        DoorRight = Instantiate(DoorRight, new Vector3(12.0f, 0.0f, 0.5f), Quaternion.identity);
    }
    //「you　die」がだんだん透明化して、最後ゲームが止まる

    
    void Update()
    {
        if (DoorState == 0)
            if ((DoorLeft.transform.position.x <= -4 || DoorRight.transform.position.x >= 4))
            {
                GameObject.Find("TextDay").GetComponent<TextDayControllr>().Toumei();
                DoorLeft.transform.localPosition = Vector3.MoveTowards(DoorLeft.transform.localPosition, new Vector3(-4.0f, 0.0f, 0.5f), step1);
                DoorRight.transform.localPosition = Vector3.MoveTowards(DoorRight.transform.localPosition, new Vector3(4.0f, 0.0f, 0.5f), step1);
                if (DoorLeft.transform.position.x >= -4 || DoorRight.transform.position.x <= 4)
                {
                    DoorState = 1;
                }
            }

        if (DoorState == 1)

        {
            DoorLeft.transform.localPosition = Vector3.MoveTowards(DoorLeft.transform.localPosition, new Vector3(-6.0f, 0.0f, 0.5f), step2);
            DoorRight.transform.localPosition = Vector3.MoveTowards(DoorRight.transform.localPosition, new Vector3(6.0f, 0.0f, 0.5f), step2);
            if (DoorLeft.transform.position.x <= -5 || DoorRight.transform.position.x >= 5)
            {
                DoorState = 2;
            }
        }
        else if (DoorState == 2)
        {
            DoorLeft.transform.localPosition = Vector3.MoveTowards(DoorLeft.transform.localPosition, new Vector3(-4.0f, 0.0f, 0.5f), step3);
            DoorRight.transform.localPosition = Vector3.MoveTowards(DoorRight.transform.localPosition, new Vector3(4.0f, 0.0f, 0.5f), step3);

            if (DoorLeft.transform.position.x >= -4 || DoorRight.transform.position.x <= 4)
            {
                DoorState = 3;
            }
        }
        else if (DoorState == 3)
        {
            Time.timeScale = 0;  //ゲームを止める
        }

    }

}

