using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaFly : MonoBehaviour {
    public GameObject ninjaFlyFrontUpperBody;
    public GameObject ninjaFlyFrontlowerBody;
    public GameObject Ninjaflyleave;



    GameObject GameController;
    
    private float AttackTimeDelay = 0.0f;
    public GameObject AttackEffect;
    bool firstAttack = false;

	void Start () {
        GameController = GameObject.Find("GameController");
        
        AttackTimeDelay = Random.Range(3.0f, 5.0f);
	}
    private void Update()
    {
        if (this.GetComponent<Floating>().isMoving==false&&firstAttack==false)
        {
            firstAttack = true;
            Invoke("attack", AttackTimeDelay);
        }
    }
    //攻撃発動
    void attack()
    {
        
        Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y, 2);
        Debug.Log(pos+ "NinjaFly");
        AttackTimeDelay = Random.Range(2.0f, 4.0f);
        
       
        Instantiate(AttackEffect,pos,Quaternion.identity);
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
        GameObject tempObject1 = Instantiate(ninjaFlyFrontUpperBody, this.transform.position, Quaternion.identity);
        tempObject1.transform.localScale = this.transform.lossyScale;
        GameObject tempObject2 = Instantiate(ninjaFlyFrontlowerBody, this.transform.position, Quaternion.identity);
        tempObject2.transform.localScale = this.transform.lossyScale;
    }
    void leave()
    {
        Destroy(gameObject);
        GameObject tempObject1 = Instantiate(Ninjaflyleave, this.transform.position, Quaternion.identity);
        tempObject1.transform.localScale = this.transform.lossyScale;
        Debug.Log(123);

    }
}
