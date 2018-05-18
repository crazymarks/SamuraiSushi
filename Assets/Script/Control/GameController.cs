using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    //魚相関
    public GameObject Maguro;        //リスト中のコードは　1
    public GameObject Tako;          //リスト中のコードは　2
    public GameObject Fugu;          //リスト中のコードは　3
    public float CreateSpeed = 2.0f;
    public Vector3 FishPosition = new Vector3(0f, -3f, 6f);
    [HideInInspector]
    public float probabilityMaguro=0.5f;  //マグロの出現確率
    [HideInInspector]
    public float probabilityTako=0.4f;    //タコの出現確率
    [HideInInspector]
    public float probabilityFugu=0.1f;    //フグの出現確率
    //魚相関

    //寿司相関
    private int SushinameCount = 0;
    public GameObject MaguroSushi;
    public GameObject TakoSushi;
    public GameObject FuguSushiGolden;
    public GameObject FuguSushiPoison;
    public GameObject SaraNormal;
    private Vector3 SaraNormalPosition=new Vector3(8.0f,-4.0f,9.0f);
    //寿司相関

    //ポイントと寿司喰う相関（start）
    public Text PopularPointText;
    public Text MoneyText;

    public List<string> CustomerList = new List<string>();//行列リスト
    static public float PofCustomers = 0.8f;  //町人がお客さんになる確率

    private int PeopleKilling;       //殺した町人の数
    public Text PeoplekillText; 
    private int Money = 0;            //この 変数　を　初期化を忘れないで！！！！！
    private bool CustomerFlag = true;   //trueの場合は次のお客さんが寿司を食べる　食べた後、一定時間内falseにする
        //価格
    private int NormalPrice = 100;
    private int GoldPrice = 200;
    private int PoisonPrice = 200;
        //価格
    private List<int> SushiList = new List<int>(); //寿司種類を表示
    /*    1 ====================Maguro-普通価格
          2 ====================Fugu--高い価格
          3 ====================poison--普通価格 （毒）                */
    private GameObject Sushi;
    private GameObject SushiDelete;
    private int SushiDeleteCount = 0;
    private float CustomerWaitTime = 1.0f;
    private GameObject peopleEatingSushi;
    private float CheckTime = 2.2f;
    //ポイントと寿司喰う相関（end）

    //ダメージ相関(start)
    public Text LifeText;
    private int Life = 3;
    public GameObject YouDied;
    //ダメージ相関(end)

    //人気値相関
    public float PopularPoint = 100;　　　//ゲーム開始の人気値
    public float SuccessPopular = 10;    //カット成功時　もらう人気値
    public float KillPopular = 30;      //町人殺して　減った人気値
    private int Combo = 0;
    public int PopularState2 = 300;     //段階２
    public int PopularState3 = 600;     //段階３
    private bool GameOverFlag = false;
   //人気値相関

//------------------------------fuction--------------------------------------------------------------------
	void Start () {
        Time.timeScale = 1;                   //ゲーム開始の時　timescaleを１に戻る（時間の流れを戻す）
        Life = 3;
        create_fish();
        CheckLoop();
        popular_decrease_with_time();
        Money = 0;
        CustomerList.Clear();
        SushiList.Clear();
    }
	
	void Update ()
    {
        PopularPointText.text = ("人気：" + PopularPoint.ToString());
        customers_list_check();
        if (PopularPoint <= 0&&GameOverFlag==false)
        {
            you_died();
            GameOverFlag = true;
        }
    }
    /// <summary>
    /// 一定時間ごとに　寿司食べれるお客さんをチェック
    /// あれば　寿司食べる事件が発生する
    /// </summary>
    void CheckLoop()
    {
        if (CustomerFlag==true)
        {
            eat_sushi();
        }
        Invoke("CheckLoop",CheckTime);
    }

    /// <summary>
    /// 毎秒人気が減少する
    /// </summary>
    void popular_decrease_with_time()
    {
        PopularPoint = PopularPoint - 1;
        Invoke("popular_decrease_with_time", 1f);
    }

    void popular_fish_cut(string result)
    {
        if(result == "success")
        {
            PopularPoint = PopularPoint + (SuccessPopular + Combo * 0.2f * SuccessPopular);   //コンボによって、人気値の増し量が上がる
            Combo = Combo + 1;
        }
        else
        {
            Combo = 0;
        }
    }

    /// <summary>
    /// 魚の生成(define amount/type/point/etc.)
    /// </summary>
    void create_fish()
    {
        //（未完成）一回出す魚の数（人気値と関係ある）
        //（未完成）一回出す魚の数（人気値と関係ある）

        //（未完成）魚種類の選択
        float fishChoose = Random.Range(0.0f, probabilityMaguro+probabilityTako+probabilityFugu) ;
        if(fishChoose < probabilityMaguro)
        {
            Instantiate(Maguro, FishPosition, Quaternion.Euler(0f, 0f, Random.Range(-45f, 45f)));
            Invoke("create_fish", CreateSpeed);
        }else if (fishChoose < (probabilityMaguro+probabilityTako))
        {
            Instantiate(Tako, FishPosition, Quaternion.Euler(0f, 0f, Random.Range(-45f, 45f)));
            Invoke("create_fish", CreateSpeed);
        }
        else if(fishChoose < (probabilityMaguro + probabilityTako+probabilityFugu))
        {
            Instantiate(Fugu, FishPosition, Quaternion.Euler(0f, 0f, Random.Range(-45f, 45f)));
            Invoke("create_fish", CreateSpeed);
        }
        else
        {
            Invoke("create_fish", CreateSpeed);
        }
        //（未完成）魚種類の選択
    }

    /// <summary>
    ///お客さんの数をチェック
    /// 最大値が人気値につれて変化する
    /// </summary>
    void customers_list_check()
    {
        if (CustomerList.Count > 5)
        {
            PofCustomers = 0.0f;
        }
        else
        {
            PofCustomers = 0.8f;
        }
    }
    /// <summary>
    /// people leave outzone
    /// </summary>
    /// <param name="name"></param>
    void people_get_out(string name)
    {
        CustomerList.Remove(name);
        customers_manage(1);
    }

    void kill_people(string name)
    {
        PeopleKilling = PeopleKilling + 1;
        PopularPoint = PopularPoint - KillPopular;     //町人を殺して、人気値が減る
        CustomerList.Remove(name);
        customers_manage(1);    
        PeoplekillText.text = ("殺人数:" + PeopleKilling.ToString());
    }
    /// <summary>
    /// お客さんリストをチェック (寿司喰う後)(int i=0)/(人を殺した後)(int i=1)
    /// </summary>
    void customers_manage(int i)
    {
        if (i == 1)      //人を殺した後、お客さんが確率で離れる
        {
            int k = CustomerList.Count-1;
            for (int j = k; j >1; j--)
            {
                float l=Random.Range(0.0f, 1.0f);
                if (l < 0.05f)
                {
                    GameObject CustomersTemp = GameObject.Find(CustomerList[j]);
                    CustomersTemp.SendMessage("customers_check1");
                    CustomerList.RemoveAt(j);
                }
            }
            for (int j = 0; j < CustomerList.Count; j++)
            {
                GameObject CustomersTemp = GameObject.Find(CustomerList[j]);
                CustomersTemp.SendMessage("customers_check2", j);
            }
        }
        if (i == 0)
        {
            for (int j = 0; j < CustomerList.Count; j++)
            {
                GameObject CustomersTemp = GameObject.Find(CustomerList[j]);
                CustomersTemp.SendMessage("customers_check2", j);
            }
        }
    }
//------------------------------create sushi-------------------------------------------
    /// <summary>
    /// 寿司リストにマグロ寿司を追加する
    /// </summary>
    /// <param name="position"></param>
    void create_magurosushi(Vector2 position)
    {
        Vector3 SushiPosition = new Vector3(position.x,position.y, 8);
        Sushi = Instantiate(MaguroSushi,SushiPosition,Quaternion.identity);
        Sushi.name = "Sushi" + SushinameCount;
        SushinameCount++;
        SushiList.Add(1);         //寿司リストにマグロ寿司(普通寿司 code 1)を追加する
    }

    /// <summary>
    /// 寿司リストにタコ寿司を追加する 
    /// </summary>
    /// <param name="position"></param>
    void create_takosushi(Vector2 position)
    {
        Vector3 SushiPosition = new Vector3(position.x, position.y, 8);
        Sushi = Instantiate(TakoSushi, SushiPosition, Quaternion.identity);
        Sushi.name = "Sushi" + SushinameCount;
        SushinameCount++;
        SushiList.Add(1);         //寿司リストにタコ寿司(普通寿司code 1)を追加する
    }
    /// <summary>
    /// 寿司リストにフグ寿司を追加する
    /// </summary>
    /// <param name="position"></param>
    void create_fugusushigolden(Vector2 position)
    {
        Vector3 SushiPosition = new Vector3(position.x, position.y, 8);
        Sushi = Instantiate(FuguSushiGolden, SushiPosition, Quaternion.identity);
        Sushi.name = "Sushi" + SushinameCount;
        SushinameCount++;
        SushiList.Add(2);         //寿司リストにゴルドー(高い寿司code 2)を追加する
    }
    /// <summary>
    /// 寿司リストにフグ寿司を追加する
    /// </summary>
    /// <param name="position"></param>
    void create_fugusushipoison(Vector2 position)
    {
        Vector3 SushiPosition = new Vector3(position.x, position.y, 8);
        Sushi = Instantiate(FuguSushiPoison, SushiPosition, Quaternion.identity);
        Sushi.name = "Sushi" + SushinameCount;
        SushinameCount++;
        SushiList.Add(3);         //寿司リストに毒寿司（code 3）を追加する
    }
    //------------------------------寿司生成---------------------------------------------------------------
    /// <summary>
    ///町人が寿司を食べる、ポイントをもらう
    /// </summary>
    void eat_sushi()
    {
        if(SushiList.Count>0 && CustomerList.Count > 0)     //最初のお客さんと最初の寿司が各リストから削除
        {
            int SushiType = SushiList[0];
            //違う寿司のポイントが違う
            switch (SushiList[0])
            {
                case 1:                                     //普通寿司
                    Money = Money + NormalPrice;
                    SaraNormalPosition.y += 0.1f;
                    SaraNormalPosition.z -= 0.00001f;
                    Instantiate(SaraNormal, SaraNormalPosition, Quaternion.identity);
                    break;
                case 2:　　　　　　　　　　　　　　　　　　　//高い寿司
                    Money = Money + GoldPrice;
                    break;
                case 3:　　　　　　　　　　　　　　　　　　　//毒寿司
                    Money = Money + PoisonPrice;
                    break;
            }
            MoneyText.text = ("金：" + Money.ToString());

            SushiList.RemoveAt(0);

            SushiDelete = GameObject.Find("Sushi" + SushiDeleteCount);  //次の寿司を探して、削除する
            Destroy(SushiDelete.gameObject);
            SushiDeleteCount++;

            //寿司を食べたお客さんを削除する
            peopleEatingSushi = GameObject.Find(CustomerList[0]);
            peopleEatingSushi.SendMessage("after_eatsushi",SushiType); //寿司のタイプのメッセージを寿司を食べた客にセンド　毒寿司なら死ぬ
            CustomerFlag = false;
            Invoke("customer_flag_set", CustomerWaitTime);　　//一定時間後次のお客さんが先頭になる
        }
    }
    /// <summary>
    /// 一定時間で寿司が食べられるフラグをリセットする
    /// </summary>
    void customer_flag_set()
    {
        CustomerFlag = true;　　　　　　　　　　
        if (CustomerList.Count>0)
        {
            CustomerList.RemoveAt(0);
        }
        customers_manage(0);
    }

    //-------------------------------------Ninja-----------------------------------------------
    /// <summary>
    /// 忍者かサムライに攻撃された　ライフが減る
    /// </summary>           
    /*void get_hurt()
    {
        Life--;
        LifeText.text = (("命：" + Life.ToString()));
        if (Life == 0)
        {
            Invoke("you_died", 1.0f);
        }
    }*/

    void you_died()　　　//プレイヤーが殺された
    {
        Vector3 pos = new Vector3(0, 0, 1);
        Instantiate(YouDied, pos, Quaternion.identity);
    }

    //-------------------------------------Ninja-----------------------------------------------
   public void reset_scene()
    {
        Application.LoadLevelAsync(0);
    }

}
