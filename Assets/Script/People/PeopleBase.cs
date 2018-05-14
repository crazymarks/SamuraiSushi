using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 町人の基礎クラス
/// </summary>
public abstract class PeopleBase : MonoBehaviour {
    protected int EnterDirection = 0;    // 0上から 1=左から 2=右から

    protected float Speed = 0f;
    public float MaxSpeed = 10f;
    public float MinSpeed = 8f;

    GameObject GameController;
    GameObject MiddleZone;
    protected float MiddleZoneX;     //middlezoneの位置　　middlezoneに当たると、町人が状態転換
    protected float MiddleZoneY;
    protected float MiddleZoneWidth;
    protected float MiddleZoneHeight;
    protected float MiddleZoneOffsetY;

    protected bool IsCustomer = false;      //お客さんかどうかを記録
    protected int CustomerNumber = -1;       //お客さんの数
    protected Vector2 RandomPoint = new Vector2(0,0);
    
    protected float ScaleRate = 0.0f;

    protected virtual void GetStart()
    {
        random_speed();
        scale_with_y();
        GameController = GameObject.Find("GameController");
        MiddleZone = GameObject.Find("MiddleZone");

        MiddleZoneWidth = MiddleZone.GetComponent<BoxCollider2D>().size.x;
        MiddleZoneHeight = MiddleZone.GetComponent<BoxCollider2D>().size.y;
        MiddleZoneOffsetY = MiddleZone.GetComponent<BoxCollider2D>().offset.y;
        
        Vector3 Pos = MiddleZone.transform.position;
        MiddleZoneX = Pos.x;
        MiddleZoneY = Pos.y;
	}
	
	protected virtual void FixedUpdate ()
    {
        float Step = Speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, RandomPoint, Step);
        scale_with_y();
	}


//-------------------------------画面外から入る----------------------------------------------
    protected virtual void enter_from_up(bool BeCustomer)
    {
        GetStart();
        float x = 0f;
        float y = 0f;
        x = Random.Range(MiddleZoneX - (MiddleZoneWidth /2),
            MiddleZoneX+(MiddleZoneWidth/2));           
        y = MiddleZoneY + MiddleZoneHeight / 2 + MiddleZoneOffsetY;
        RandomPoint = new Vector2(x, y);
        EnterDirection = 0; //上から
        IsCustomer = BeCustomer;   // お客さんです！
    }

    protected virtual void enter_from_left(bool BeCustomer)
    {
        GetStart();
        float x = 0f;
        float y = 0f;
        x = MiddleZoneX - MiddleZoneWidth / 2;
        y = Random.Range(MiddleZoneY - MiddleZoneHeight / 2 + MiddleZoneOffsetY,
            MiddleZoneX + MiddleZoneHeight / 2 + MiddleZoneOffsetY);           
        RandomPoint = new Vector2(x, y);
        EnterDirection = 1; //左から
        IsCustomer = BeCustomer;   // お客さんです！
    }

    protected virtual void enter_from_right(bool BeCustomer)
    {
        GetStart();
        float x = 0f;
        float y = 0f;
        x = MiddleZoneX + MiddleZoneWidth / 2;
        y = Random.Range(MiddleZoneY - MiddleZoneHeight / 2 + MiddleZoneOffsetY,
            MiddleZoneX + MiddleZoneHeight / 2 + MiddleZoneOffsetY);
        RandomPoint = new Vector2(x, y);
        EnterDirection = 2; //右から
        IsCustomer = BeCustomer;   //  お客さんです！
    }
    //-------------------------------画面外から入る---------------------------------------------

    //--------------------------------画面外に移動-----------------------------------------------    
    protected virtual void exit_to_up()
    {
        float x = 0f;
        float y = 0f;
        x = Random.Range(MiddleZoneX - PeopleCreate.TopCreateVar,
            MiddleZoneX + PeopleCreate.TopCreateVar);
        y = PeopleCreate.MaxY;
        RandomPoint = new Vector2(x, y);
    }
    protected virtual void exit_to_left()
    {
        float x = 0f;
        float y = 0f;
        x = -(PeopleCreate.MaxX);
        y = Random.Range(MiddleZoneY - MiddleZoneHeight / 2+MiddleZoneOffsetY,
            MiddleZoneX + MiddleZoneHeight / 2 +MiddleZoneOffsetY);
        RandomPoint = new Vector2(x, y);
    }
    protected virtual void exit_to_right()
    {
        float x = 0f;
        float y = 0f;
        x = PeopleCreate.MaxX;
        y = Random.Range(MiddleZoneY - MiddleZoneHeight / 2 + MiddleZoneOffsetY,
            MiddleZoneX + MiddleZoneHeight / 2 + MiddleZoneOffsetY);
        RandomPoint = new Vector2(x, y);
    }
    //-------------------------------画面外に移動----------------------------------------------

    protected virtual void state_change()
    {
        if (IsCustomer)
        {
            customers_move(CustomerNumber);
        }else
        {
            if (EnterDirection == 0)                //上から来た
            {
                float NextDirection = Random.Range(0.0f, 1.0f);
                if (NextDirection < 0.5f)
                {       exit_to_right(); }
                else
                {       exit_to_left();  }
            }
            else if (EnterDirection == 1)         //左から来た
            {
                float NextDirection = Random.Range(0.0f, 1.0f);
                if (NextDirection < 0.5)
                {    exit_to_right();    }
                else
                {    exit_to_up();       }
            }
            else                                 //右から来た
            {
                float NextDirection = Random.Range(0f, 1f);
                if (NextDirection < 0.5)
                {    exit_to_left();     }
                else
                {    exit_to_up();       }
            }
        }
    }

    /// <summary>
    ///行列を作るため、位置を何番目によって指定する
    /// </summary>
    protected virtual void customers_move(int number)
    {
        float y = number * 0.2f - 3;
       RandomPoint =new Vector2(0,y);
    }
    /// <summary>
    ///お客さんリストから削除、状態を変更する
    /// </summary>
    protected virtual void customers_check1()
    {
        IsCustomer = false;
        state_change();
    }
    protected virtual void customers_check2(int number)
    {
        CustomerNumber = number;
        state_change();
    }
    /// <summary>
    /// 殺されたアニメーション
    /// gamecontrollerに殺されたメッセージをセンド、そっちのリストから削除する
    /// </summary>
    protected virtual void be_killed()
    {
        //アニメーション（未完成）
        //アニメーション（未完成）
        GameController.SendMessage("kill_people", this.name);
        Destroy(gameObject);
    }
    //画面外へ行った
    protected virtual void get_out()
    {
        GameController.SendMessage("people_get_out", this.name);
        Destroy(gameObject);
    }
    //毒殺された
    protected virtual void killed_by_poison()
    {
        //アニメーション（未完成）
        //アニメーション（未完成）
        GameController.SendMessage("kill_people", this.name);
        Destroy(gameObject);
    }

    //状態変更
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "MiddleZone")
        {
            state_change();
        }
        if (other.name == "Line")
        {
            if(this.transform.position.x != 0 || CustomerNumber ==0)   
                be_killed();
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "OutZone")
        {
            get_out();
        }
    }
    //移動スピードをランダムにする
    protected virtual void random_speed()
    {
        Speed = Random.Range(MinSpeed, MaxSpeed);
    }
    //y値につれて、大きさが変化する
    protected virtual void scale_with_y()
    {
        ScaleRate = (this.transform.position.y - 1) * -0.15f;
        this.transform.localScale = new Vector3(ScaleRate + 0.05f, ScaleRate, 1);

        Vector3 Pos = this.transform.position;

        Pos.z = Pos.y + 15f;
        transform.position = Pos;
    }
}
