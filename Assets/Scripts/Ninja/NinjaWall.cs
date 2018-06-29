using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaWall : MonoBehaviour {
    public GameObject ninjaWallFrontUpperBody;
    public GameObject ninjaWallFrontlowerBody;

    private float WaitTime = 0.3f;
    GameObject GameController;
    LifeCounter lifeCounter;
    private float AttackTimeDelay = 0.0f;
    public GameObject AttackEffect;

    private float ScaleRate;

    void Start()
    {
        GameController = GameObject.Find("GameController");
        lifeCounter = GameObject.Find("Lifes").GetComponent<LifeCounter>();
        AttackTimeDelay = Random.Range(3.0f, 5.0f);
        Invoke("attack", AttackTimeDelay);
    }

    //攻撃発動
    void attack()
    {
        lifeCounter.Damage();
        AttackTimeDelay = Random.Range(2.0f, 4.0f);
        Vector3 pos = new Vector3(0, 0, 2);
        Instantiate(AttackEffect, pos, Quaternion.identity);
        Invoke("attack", AttackTimeDelay);
    }
    //切られた
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Line")
        {
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
    protected virtual void scale_with_y()
    {
        
        ScaleRate = (this.transform.position.y - 1) * -0.15f;
        this.transform.localScale = new Vector3(ScaleRate + 0.05f, ScaleRate, 1);

        Vector3 Pos = this.transform.position;

        Pos.z = Pos.y + 15f;
        transform.position = Pos;
    }
}
