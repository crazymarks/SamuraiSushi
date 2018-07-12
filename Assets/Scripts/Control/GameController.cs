using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public AudioSource mainBGM;
	
	//タイマー相関
	Timer timer = new Timer();

    //魚相関
    public GameObject Maguro;        //リスト中のコードは　1
    public GameObject Tako;          //リスト中のコードは　2
    public GameObject Fugu;          //リスト中のコードは　3
    public Vector3 FishPosition = new Vector3(0f, -3f, 6f);
    [SerializeField, HeaderAttribute("最初の魚の生成時間")]
    public float firstFishCreateTime = 1f;  //最初の生成時間
    [SerializeField, HeaderAttribute("魚の生成間隔")]
    public float CreateSpeed = 2.0f;   
    [SerializeField, HeaderAttribute("魚の出現確率だが、自分で百分比を計算 例：Maguroは M/(M+T+F)")]
    public  float probabilityMaguroTest = 0.5f;  //マグロの出現確率 テスト用
    public  float probabilityTakoTest = 0.4f;    //タコの出現確率　テスト用
    public  float probabilityFuguTest = 0.1f;    //フグの出現確率　テスト用

    public static float probabilityMaguro=0.5f;  //マグロの出現確率
    public static float probabilityTako=0.4f;    //タコの出現確率
    public static float probabilityFugu=0.1f;    //フグの出現確率
    [SerializeField, HeaderAttribute("クリアに必要な魚の数")]
    public int maxFishAmount = 40;  //クリアに必要な切った魚の尾数
    private int cuttedFishAmount = 0;  //切った魚の数
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
    public Text MoneyText;

    [HideInInspector]
    public List<string> CustomerList = new List<string>();//行列リスト
    static public float PofCustomers = 0.8f;  //町人がお客さんになる確率
    public float PofCustomersTest = 0.6f;//町人がお客さんになる確率 テスト用

    private int PeopleKilling;       //殺した町人の数
    public Text PeoplekillText;

    [SerializeField, HeaderAttribute("人間性最大値、一人-1")]
    public int maxHumanity = 20;     //人間性
    private int currentHumanity;
    public Image humanityGage;

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
    public GameObject Win;
    public GameObject KillerEnding;
    public GameObject NinjyaEnding;
    private bool GameoverFlag = false;
    //ダメージ相関(end)

    //combo相関
    private int Combo = 0;
    bool isHeal = true;
    public GameObject lifeCounter;
    public Text ComboText;
    public Image popularGage;
   //combo相関

//------------------------------fuction--------------------------------------------------------------------
	void Start () {
        Time.timeScale = 1;                   //ゲーム開始の時　timescaleを１に戻る（時間の流れを戻す）
        Life = 3;
       Invoke("create_fish",firstFishCreateTime);  //魚が一定時間後生成
        CheckLoop();
        Money = 0;
        cuttedFishAmount = 0;
        currentHumanity = maxHumanity;
        CustomerList.Clear();
        SushiList.Clear();
        mainBGM.Play();
        lifeCounter = GameObject.Find("Lifes") ;
    }
	
	void Update ()
    {
		//タイマー更新
		timer.Update();
		
        //テスト用　正式版消す
        probabilityMaguro = probabilityMaguroTest;  
        probabilityTako = probabilityTakoTest;    
        probabilityFugu = probabilityFuguTest;
        PofCustomers = PofCustomersTest;  //お客さんになる確率
        //テスト用　正式版消す
        customers_list_check();

        popularGage.fillAmount = (float)cuttedFishAmount / maxFishAmount;  //切った魚の数に変更      
        if (cuttedFishAmount >= maxFishAmount && GameoverFlag == false)   //金は一定に達成すると、ゲームクリア
        {
            GameoverFlag = true;
            GameClear();
        }
        if (currentHumanity <= 0 && GameoverFlag == false)    //人間性がなくなったら、ゲームオーバー
        {
            GameoverFlag = true;
            GameOver2();
        }

        if(Combo != 0 && Combo % 5 == 0 && isHeal)
        {
            lifeCounter.GetComponent<LifeCounter>().ComboHeal();   
            isHeal = false;
        }
        else if(Combo % 5 != 0)
        {
            isHeal = true;
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
    /// 
    /// </summary>
    /// <param name="result"></param>
    void ComboCheck(string result)
    {
        if(result == "success")
        {
            Combo = Combo + 1;
        }
        else
        {
            Combo = 0;
        }
        ComboText.text = ("Combo:" + Combo.ToString());       
    }

    /// <summary>
    /// 魚の生成(define amount/type/point/etc.)
    /// </summary>
    void create_fish()
    {
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
        CustomerList.Remove(name);
        customers_manage(1);    
        PeoplekillText.text = ("殺人数:" + PeopleKilling.ToString());
        currentHumanity -= 1;  //殺人で人間性がなくなる   
        humanityGage.fillAmount -= 1f / maxHumanity;
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
        cuttedFishAmount += 1;   //切った魚の尾数＋１
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
        cuttedFishAmount += 1;   //切った魚の尾数＋１
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
        cuttedFishAmount += 1;   //切った魚の尾数＋１
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
        cuttedFishAmount += 1;   //切った魚の尾数＋１
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

    //-------------------------------------ゲームオーバー-----------------------------------------------

     public void GameOver1()　　　//プレイヤーが殺されたによるエンディング
    {
        Time.timeScale = 0.1f;
        Invoke("GameOverNinjya", 0.2f);
        mainBGM.Stop();
        GameObject.Find("SEPlayer").GetComponent<PlaySE>().NinjyaWinEnd();
    }
    void GameOverNinjya()   //「閉店」の画面が少し遅延して出す
    {
        Instantiate(NinjyaEnding, new Vector3(0, 0, 1), Quaternion.identity);
    }

    void GameOver2()     //町人を殺しすぎて、人間性がなくなったゲームオーバー
    {
        Time.timeScale = 0.1f;
        Invoke("GameOverKiller", 0.2f);
        mainBGM.Stop();
        GameObject.Find("SEPlayer").GetComponent<PlaySE>().KillerWinEnd();
    }
    void GameOverKiller()   //「閉店」の画面が少し遅延して出す
    {
        Instantiate(KillerEnding, new Vector3(0, 0, 1), Quaternion.identity);
    }

    //-------------------------------------ゲームオーバー-----------------------------------------------

    void GameClear()          //一定寿司が町人に食べさせると、ゲームクリア
    {
        Vector3 pos = new Vector3(0, 0, 1);
        Instantiate(Win, pos, Quaternion.identity);
        mainBGM.Stop();
        GameObject.Find("SEPlayer").GetComponent<PlaySE>().WinEnd();
    }

    public void reset_scene()
    {
        Application.LoadLevelAsync(0);
    }

}
