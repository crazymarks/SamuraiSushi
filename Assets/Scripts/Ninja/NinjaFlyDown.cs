using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaFlyDown : MonoBehaviour {
    public GameObject ninjaFlyFrontUpperBody;
    public GameObject ninjaFlyFrontlowerBody;
    public GameObject ninjaleave;
    public GameObject Frame;
    private GameObject deleteFrame;  //生成するフレーム削除ため

    GameObject GameController;
    private float AttackTimeDelay = 0.0f;
    public GameObject AttackEffect;
    bool firstAttack = false;
    private bool leaveFlag = false;

    void Start()
    {
        GameController = GameObject.Find("GameController");
    }

    private void Update()
    {
        if (this.GetComponent<Floating2>().isMoving == false && firstAttack == false)
        {
            firstAttack = true;
            Invoke("Frame1", 2f);
        }
        if (leaveFlag == true)
        {
            this.GetComponent<Floating2>().enabled = false;
            float step = 3f * Time.deltaTime;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, gameObject.transform.position + new Vector3(0f, 10f, 0f), step);
            if (this.transform.position.y > 8f)
            {
                Destroy(this.gameObject);
            }
        }
    }
    //攻撃発動
    void attack()
    {
        OnDestroy(); //フレームを消す
        Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y, 2);       
        Instantiate(AttackEffect, pos, Quaternion.identity);

        Invoke("leave2", 1.5f);
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
    void leave2()
    {
        leaveFlag = true;
    }

    //忍者を見つけるやすくするサポート
    void Frame1()
    {
        Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y, 2);

        deleteFrame = Instantiate(Frame, pos, Quaternion.identity);
        //frame 出来たら攻撃が始める
        AttackTimeDelay = Random.Range(2.0f, 3.0f);
        Invoke("attack", AttackTimeDelay);
    }

    private void OnDestroy()//殺されたらフレームを消す
    {
        if (deleteFrame != null)
        {
            Destroy(deleteFrame);
        }
    }
}

