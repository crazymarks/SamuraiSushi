using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaFlyDown : MonoBehaviour {
    public GameObject ninjaFlyFrontUpperBody;
    public GameObject ninjaFlyFrontlowerBody;
    public GameObject Frame;//frame周

    private float WaitTime = 0.3f;
    GameObject GameController;
    LifeCounter lifeCounter;
    private float AttackTimeDelay = 0.0f;
    public GameObject AttackEffect;
    private GameObject deleteFrame;//Destroy frame 周
    bool firstAttack = false;
    void Start()
    {
        GameController = GameObject.Find("GameController");
        lifeCounter = GameObject.Find("Lifes").GetComponent<LifeCounter>();
        AttackTimeDelay = Random.Range(3.0f, 5.0f);
    }
    private void Update()
    {
        if (this.GetComponent<Floating2>().isMoving == false && firstAttack == false)
        {//---------

            Frame1();

            //-----------
            //firstAttack = true;
            //Invoke("attack", AttackTimeDelay);
        }
    }
    //-------------------
    //忍者を見つけるやすくするサポート
    void Frame1()
    {
        Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y, 2);

        deleteFrame = Instantiate(Frame, pos, Quaternion.identity);
        //frame 出来たら攻撃が始める
        AttackTimeDelay = Random.Range(2.0f, 4.0f);
        Invoke("attack", AttackTimeDelay);
    }
    private void OnDestroy()//サポート終了
    {
        Destroy(deleteFrame);
    }
    //----------------------------
    //攻撃発動
    void attack()
    {
        lifeCounter.Damage();
        Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y, 2);
        
        Instantiate(AttackEffect, pos, Quaternion.identity);
        Debug.Log(pos + "NinjaFlyDown");
       
    }
    //切られた
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Line")
        {
            GameObject.Find("SEPlayer").GetComponent<PlaySE>().KillNinja();
            die();
        }
    }
    void die()
    {
        Destroy(gameObject);
        GameObject tempObject1 = Instantiate(ninjaFlyFrontUpperBody, this.transform.position, Quaternion.identity);
        tempObject1.transform.localScale = this.transform.lossyScale;
        GameObject tempObject2 = Instantiate(ninjaFlyFrontlowerBody, this.transform.position, Quaternion.identity);
        tempObject2.transform.localScale = this.transform.lossyScale;
    }
}

