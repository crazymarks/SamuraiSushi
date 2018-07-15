using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 町人の基礎クラス
/// </summary>
public abstract class PeopleBase : MonoBehaviour {
    protected int EnterDirection = 0;    // 0上から 1=左から 2=右から
     public enum MoveStatus  //町人の各移動状態　変数名は向き
    {
        Back=1,　　　　　　　
      　Front=2,
        Left=3,
        Right=4
    }
    //横最大   18
    //横普通   4
    //横最低   2
    //縦最大   9
    //縦普通   3
    //縦最低   1
    protected float speed = 0f;
    protected float horrizontalSpeed =0f;
    protected float verticalSpeed = 0f;

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

    int SpriteState=0;

    Quaternion rotation = Quaternion.identity;     //回転のアニメーション用
    bool isRotate = false;     //回転の状態
    float rotateY = 0f; //回転のy値


    //----------------------------------------------------------------------------------------------------
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

        SpriteInitalize();      
	}
	
	protected virtual void FixedUpdate ()
    {
        if (isRotate)
        {
            RotationAnime();
        }
        else
        {
            switch (SpriteState)
            {
                case (int)MoveStatus.Left:
                case (int)MoveStatus.Right:
                    speed = horrizontalSpeed;
                    break;
                case (int)MoveStatus.Back:
                case (int)MoveStatus.Front:
                    speed = verticalSpeed;
                    break;
            }
            float Step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, RandomPoint, Step);
            scale_with_y();
        }       
        if (IsCustomer == true&&new Vector2( transform.position.x, transform.position.y) ==RandomPoint)
        {
            if (CustomerNumber == 0)
            {
                GameController.GetComponent<GameController>().customerflag_set(true);
            }
            SpriteState = (int)MoveStatus.Front; //向き
            SpriteChange();
        }
	}

//-------------------------------画面外から入る----------------------------------------------
    protected virtual void enter_from_up(bool BeCustomer)
    {
        SpriteState = (int)MoveStatus.Front; //向き
        SpriteInitalize();
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
        SpriteState = (int)MoveStatus.Right;//向き
        SpriteInitalize();
        GetStart();
        float x = 0f;
        float y = 0f;
        x = MiddleZoneX - MiddleZoneWidth / 2;
        y = Random.Range(MiddleZoneY - MiddleZoneHeight / 2 + MiddleZoneOffsetY,
            MiddleZoneY + MiddleZoneHeight / 2 + MiddleZoneOffsetY);           
        RandomPoint = new Vector2(x, y);
        EnterDirection = 1; //左から
        IsCustomer = BeCustomer;   // お客さんです！
    }

    protected virtual void enter_from_right(bool BeCustomer)
    {
        SpriteState = (int)MoveStatus.Left;//向き
        SpriteInitalize();
        GetStart();
        float x = 0f;
        float y = 0f;
        x = MiddleZoneX + MiddleZoneWidth / 2;
        y = Random.Range(MiddleZoneY - MiddleZoneHeight / 2 + MiddleZoneOffsetY,
            MiddleZoneY + MiddleZoneHeight / 2 + MiddleZoneOffsetY);
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
            MiddleZoneY + MiddleZoneHeight / 2 +MiddleZoneOffsetY);
        RandomPoint = new Vector2(x, y);
    }
    protected virtual void exit_to_right()
    {
        float x = 0f;
        float y = 0f;
        x = PeopleCreate.MaxX;
        y = Random.Range(MiddleZoneY - MiddleZoneHeight / 2 + MiddleZoneOffsetY,
            MiddleZoneY + MiddleZoneHeight / 2 + MiddleZoneOffsetY);
        RandomPoint = new Vector2(x, y);
    }
    //-------------------------------画面外に移動----------------------------------------------

    public virtual void state_change()
    {
        if (IsCustomer)
        {
            customers_move(CustomerNumber);

        }
        else
        {
            if (EnterDirection == 0)                //上から来た
            {
                float NextDirection = Random.Range(0.0f, 1.0f);
                if (NextDirection < 0.5f)
                {
                    exit_to_right();
                    SpriteState =(int) MoveStatus.Right;//向き
                    isRotate = true;//回転させる
                }
                else
                {
                    exit_to_left();
                    SpriteState = (int)MoveStatus.Left;//向き
                    isRotate = true;//回転させる
                }
            }
            else if (EnterDirection == 1)         //左から来た
            {
                float NextDirection = Random.Range(0.0f, 1.0f);
                if (NextDirection < 0.5)
                {
                    exit_to_right();
                    SpriteState = (int)MoveStatus.Right;//向き
                    SpriteChange();
                }
                else
                {
                    exit_to_up();
                    SpriteState = (int)MoveStatus.Back;//向き
                    isRotate = true;//回転させる
                }
            }
            else                                 //右から来た
            {
                float NextDirection = Random.Range(0f, 1f);
                if (NextDirection < 0.5)
                {
                    exit_to_left();
                    SpriteState = (int)MoveStatus.Left;//向き
                    SpriteChange();
                }
                else
                {
                    exit_to_up();
                    SpriteState = (int)MoveStatus.Back;//向き
                    isRotate = true;//回転させる
                }
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
        if (IsCustomer == true)
        {
            if (CustomerNumber == 0)
            {
                GameController.GetComponent<GameController>().customerflag_set(false);
            }
            IsCustomer = false;
            state_change();
        }

    }
    protected virtual void customers_check2(int number)
    {
        CustomerNumber = number;
        if (IsCustomer == true)
        {
            state_change();
        }
    }
    /// <summary>
    /// 殺されたアニメーション
    /// gamecontrollerに殺されたメッセージをセンド、そっちのリストから削除する
    /// </summary>
    protected virtual void be_killed()
    {
        GameObject.Find("SEPlayer").GetComponent<PlaySE>().KillPeople();    //SE再生
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
        GameController.SendMessage("kill_people", this.name);
        Destroy(gameObject);
    }

    //状態変更
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

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

    /// <summary>
    ///  移動スピードをランダムにする
    ///横最大   18
    ///横普通   4
    ///横最低   2
    ///縦最大   9
    ///縦普通   3
    ///縦最低   1
    /// </summary>
    public virtual void random_speed()
    {
        float x = Random.Range(0f, 1f);

        if (x < GameObject.Find("GameController").GetComponent<PeopleCreate>().SlowSpeedProb)//もしｘが最小速度の確率より小さいならば、速度を最小に設置
        {
            verticalSpeed = 1f;
            horrizontalSpeed = 2f;
        }
        else if (x > GameObject.Find("GameController").GetComponent<PeopleCreate>().FastSpeedProb)//もしｘが最大速度の確率より大きいならば、速度を最大に設置
        {
            verticalSpeed = 9f;
            horrizontalSpeed = 18f;
        }
        else　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　//でなければ、普通のスビートに設置
        {
            verticalSpeed = 3f;
            horrizontalSpeed = 4f;
        }
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
    /// <summary>
    /// 最初の方向の画像に変わる
    /// </summary>
    protected virtual void  SpriteInitalize()
    {

    }
    /// <summary>
    /// 方向状態変化
    /// </summary>
    protected virtual void SpriteChange()
    {

    }

    public int GetSpriteState()
    {
        return SpriteState;
    }

    /// <summary>
    /// 回転のアニメーション
    /// </summary>
    protected virtual void RotationAnime()
    {
        if (rotateY < 80)
        {
            //90°回転させる
            rotateY += 3;
            rotation.eulerAngles = new Vector3(0, this.transform.rotation.eulerAngles.y + 3, 0);
            this.transform.rotation = rotation;
        }
        else if (rotateY < 85)
        {
            SpriteChange();
            rotateY += 3;
            rotation.eulerAngles = new Vector3(0, this.transform.rotation.eulerAngles.y - 3, 0);
            this.transform.rotation = rotation;

        }
        else if (rotateY < 160)
        {
            rotateY += 3;
            rotation.eulerAngles = new Vector3(0, this.transform.rotation.eulerAngles.y - 3, 0);
            this.transform.rotation = rotation;
        }
        else
        {
            rotateY = 0;
            isRotate = false;
        }
    }
}
