using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaWall : MonoBehaviour {
    public GameObject ninjaWallFrontUpperBody;
    public GameObject ninjaWallFrontlowerBody;
    public GameObject ninjaleave;
    public GameObject Frame;
    private GameObject deleteFrame;//生成するフレーム削除ため

    private float WaitTime = 0.3f;
    GameObject GameController;  
    private float AttackTimeDelay = 0.0f;
    public GameObject AttackEffect;

    private float ScaleRate;    //大きさ変化

    void Start()
    {
        GameController = GameObject.Find("GameController");
        Invoke("Frame1", 2f);
    }

    private void Update()
    {
        scale_with_x();
    }
    //攻撃発動
    void attack()
    {
        OnDestroy(); //フレームを消す
        Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y, 2);
        Instantiate(AttackEffect, pos, Quaternion.identity);

        Invoke("leave", 1.5f);
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
    void leave()
    {
        Destroy(gameObject);
        GameObject tempObject1 = Instantiate(ninjaleave, this.transform.position+new Vector3(0f,1.1f,0f), Quaternion.identity);
        tempObject1.transform.localScale = this.transform.lossyScale*8f;

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

    void Frame1()
    {
        //transform.position.xを正しく場所にするため
       Vector3 pos = new Vector3(this.transform.position.x+0.06f, this.transform.position.y +10.24f*this.transform.localScale.y*0.25f, 2);

        //Frameを作り
      deleteFrame = Instantiate(Frame, pos, Quaternion.identity);

        deleteFrame.transform.localScale = new Vector3(this.transform.localScale.x *deleteFrame.transform.localScale.x, 
            this.transform.localScale.y * deleteFrame.transform.localScale.y, 2.0f);
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
//+-6--0.5
//+-1--0.2

//y=ax+b
//0.5=+-6a+b
//0.2=+-1a+b
//0.3=5a
//a=0.3/5=0.06
//b=0.2-a=0.2-0.06=0.14
//scale=
