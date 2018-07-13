using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaWall : MonoBehaviour {
    public GameObject ninjaWallFrontUpperBody;
    public GameObject ninjaWallFrontlowerBody;
    public GameObject Frame;//frame周

    private float WaitTime = 0.3f;
    GameObject GameController;
    LifeCounter lifeCounter;
    private float AttackTimeDelay = 0.0f;
    public GameObject AttackEffect;
    private GameObject deleteFrame;//Destroy frame 周

    private float ScaleRate;    //大きさ変化
  
    int zhale = 1;
    void Start()
    {
        GameController = GameObject.Find("GameController");
        lifeCounter = GameObject.Find("Lifes").GetComponent<LifeCounter>();

       
       
    }
    private void Update()
    {
        scale_with_x();
        if(this.transform.lossyScale.x <= 0 || zhale <= 1)
            {
            Frame1();//忍者を見つけるやすくするサポート
            zhale = 2;

        }
       
    }
    //-------------------
   
    //攻撃発動
    void attack()
    {
       
        lifeCounter.Damage();
        Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y, 2);
        Instantiate(AttackEffect, pos, Quaternion.identity);
       
        Debug.Log(pos + "NinjaWall");
        AttackTimeDelay = Random.Range(4.0f, 4.0f);

        Invoke("attack", AttackTimeDelay);
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
        GameObject tempObject1 = Instantiate(ninjaWallFrontUpperBody, this.transform.position, Quaternion.identity);
        tempObject1.transform.localScale = this.transform.lossyScale;
        GameObject tempObject2 = Instantiate(ninjaWallFrontlowerBody, this.transform.position, Quaternion.identity);
        tempObject2.transform.localScale = this.transform.lossyScale;
    }
    
    /// <summary>
    /// y値につれて、大きさが変化する
    /// </summary>
    protected virtual void scale_with_x()
    {
        ScaleRate = Mathf.Abs(this.transform.position.x) * 0.06f + 0.14f;
        this.transform.localScale = new Vector3(ScaleRate, ScaleRate, 1);

        Vector3 Pos = this.transform.position;
        Pos.z = Pos.y + 15f;
        transform.position = Pos;
    }
    //+-6--0.5
    //+-1--0.2

    //y=ax+b
    //0.5=+-6a+b
    //0.2=+-1a+b
    //0.3=5a
    //a=0.3/5=0.06
    //b=0.2-a=0.2-0.06=0.14
    //scale=

    //忍者を見つけるやすくするサポート
    float y;
    void Frame1()
    {
        //this.transform.localScale.yが負数ではないため
        y = this.transform.localScale.y;
        if (y < 0)
        {
            y = y * -1;
        }
            //transform.position.xを正しく場所にするため
        Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y +y*2.5f, 2);

        //Frameを作り
        deleteFrame = Instantiate(Frame, pos, Quaternion.identity);
  
        Frame.transform.localScale = new Vector3(this.transform.localScale.x *7.5f, this.transform.localScale.y * 10, 2.0f);
        Debug.Log(this.transform.localScale.x * 10);
        //frame 出来たら攻撃が始める
        AttackTimeDelay = Random.Range(3.0f, 5.0f);
        Invoke("attack", AttackTimeDelay);
    }
    private void OnDestroy()//サポート終了
    {
        Destroy(deleteFrame);
    }
    //----------------------------
}
