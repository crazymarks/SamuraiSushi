using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour {
    float speed = 3; //Move移動の速さ
    float radian = 0; // 弧度  
    float perRadian = 0.03f; // 变化の弧度  
    float radius = 0.2f; // 浮く範囲  
    [HideInInspector]
    public bool isMoving = true;   //移動中　trueの場合は目標点に到着
    Vector3 targetPoint;   //目標点の座標
    Vector3 leavetargetPoint;
    // Use this for initialization  
    void Start()
    {
        targetPoint = new Vector3(Random.Range(-3f, 3f), Random.Range(2.5f, 4f), gameObject.transform.position.z);      //ランダムに目標点を生成
        
    }

    

    void Move()
    {
        float step = speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,targetPoint, step);
        
    }
    void floatninja()
    {
        radian += perRadian; // 弧度を0.03ずつ増える  
            float dy = Mathf.Sin(radian) * radius; // dyはx轴の関数
    transform.position = targetPoint + new Vector3(dy, 0, 0);
    }
    void FixedUpdate()
    {

        if (isMoving) 
        {
            Move();
        }
        else
        {
            floatninja();
        }
        if (transform.position==targetPoint)
        {
            isMoving = false;
        }

    }




}

