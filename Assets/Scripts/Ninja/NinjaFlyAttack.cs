using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaFlyAttack : MonoBehaviour {
    private float Speed = 500.0f;
    private float ScaleRate1 = 0.5f;
    private float ScaleRate2 = 0.1f;
    LifeCounter lifeCounter;

    //Damageアニメ
    public GameObject Damage;


    int n ;
    float i;
    //private float step1;    //使われていない
    //float speed = 50;       //使われていない
    private float ScaleRate3 = 0.1f;
    void Start () {
        lifeCounter = GameObject.Find("Lifes").GetComponent<LifeCounter>();
        //step1 = speed * Time.deltaTime;
        Invoke("go_die",1.0f);
        
    }

    
    

    public bool isMoving = true;//移動中　trueの場合は目標点に到着
    bool firstAttack = false;//移動中　trueの場合は目標点に到着
    void Update()
    {
        ScaleRate1 = ScaleRate1 + 0.025f;//ｘ大きくする速度
        ScaleRate2 = ScaleRate2 + 0.01f; //ｙ大きくする速度
        ScaleRate3 = ScaleRate3 - 0.05f;
        if (this.GetComponent<NinjaFlyAttack>().isMoving == false && firstAttack == false)

        {
            firstAttack = true;
            n = Random.Range(0, 180);//（手裏剣最初のｚ）
            this.transform.Rotate(this.transform.position.x, this.transform.position.y, n);
            
             
        }
        if (transform.position.y > -1)
        {
            transform.position = new Vector3(this.transform.position.x, this.transform.position.y-0.0005f, 1);//y下げる
        }

        
        transform.localScale = new Vector3(ScaleRate1, ScaleRate2, 1);//xy大きくする
       
       

    }
  
    void go_die()
    {
        Destroy(this.gameObject);
        lifeCounter.Damage();
        GameObject DamageObject = Instantiate(Damage, new Vector3(0f, 0f, 0.6f), Quaternion.identity);
    }
}
