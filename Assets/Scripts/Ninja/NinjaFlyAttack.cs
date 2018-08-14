using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaFlyAttack : MonoBehaviour {
    private float Speed = 500.0f;
    private float ScaleRate1 = 0.5f;
    private float ScaleRate2 = 0.1f;
    private float countTime = 0f;   //ダメージ受けるまでの時間
    private Vector3 counteredPoint; //接触した位置
    private Vector3 counteredPoint2;　   //接触する時の位置
    private bool beCountered = false; //切られたかどうか
    LifeCounter lifeCounter;

    //Damageアニメ
    public GameObject Damage;


    int n ;
    //private float step1;    //使われていない
    //float speed = 50;       //使われていない

    void Start () {
        lifeCounter = GameObject.Find("Lifes").GetComponent<LifeCounter>();
        n = Random.Range(0, 180);//（手裏剣最初のｚ）
        this.transform.Rotate(0f, 0f, n);
    }

    
    

    public bool isMoving = true;//移動中　trueの場合は目標点に到着
    bool firstAttack = false;//移動中　trueの場合は目標点に到着
    void Update()
    {
        if(beCountered == false)
        {
            ScaleRate1 = ScaleRate1 + 0.025f;//ｘ大きくする速度
            ScaleRate2 = ScaleRate2 + 0.01f; //ｙ大きくする速度
            if (transform.position.y > -1)
            {
                transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.0005f, 2);//y下げる
            }
            transform.localScale = new Vector3(ScaleRate1, ScaleRate2, 1);//xy大きくする

            countTime = countTime + Time.deltaTime;
            if (countTime > 1f)
            {
                go_die();
            }
        }
        else
        {
            ScaleRate1 = ScaleRate1 * 0.93f;//ｘ大きくする速度
            ScaleRate2 = ScaleRate2 * 0.93f; //ｙ大きくする速度

          transform.position =Vector3.MoveTowards(gameObject.transform.position,(counteredPoint2-counteredPoint)+ gameObject.transform.position, 5f*Time.deltaTime);//y下げる

            transform.localScale = new Vector3(ScaleRate1, ScaleRate2, 1);//xy大きくする

            if (this.transform.localScale.y < 0.01f || this.transform.localScale.x < 0.01f)
            {
                Destroy(this.gameObject);
            }
        }


    }
  
    void go_die()
    {
        Destroy(this.gameObject);
        lifeCounter.Damage();
        GameObject DamageObject = Instantiate(Damage, new Vector3(0f, 0f, 0.6f), Quaternion.identity);
    }


     void OnTriggerEnter2D(Collider2D other)
    {

        if (other.name == "Line")
        {
            if (beCountered == false)
            {
                if (GameObject.Find("Katana").GetComponent<KatanaMouse>().enabled==true)
                {
                    counteredPoint = Input.mousePosition;
                }
                else if(GameObject.Find("Katana").GetComponent<Katana>().enabled == true)
                {
                    counteredPoint = Input.GetTouch(0).position;
                }

                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 11f);
                counteredPoint2 = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0);
                counteredPoint = new Vector3 (Camera.main.ScreenToWorldPoint(counteredPoint).x, Camera.main.ScreenToWorldPoint(counteredPoint).y,0);
                
            }
            beCountered = true;

        }
    }
}
