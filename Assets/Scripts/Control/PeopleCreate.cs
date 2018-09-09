using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleCreate : MonoBehaviour {

    //生成位置相関
    private Vector2 PeopleCreatePoint;
    static public float MaxY = 0.5f;        //スクリーンの最大Y値　人の生成で使う
    static public float MaxX = 12f;       //スクリーンの最大x値　人の生成で使う
    static public float TopCreateVar = 4f; //上の生成区の範囲指定用
    public float MaxYTest = 0.5f;        //test
    public float MaxXTest = 12f;       //test
    public float TopCreateVarTest = 4f; //test
    GameObject MiddleZone;
    //生成位置相関

    //町人相関
    public GameObject Man;           //一般人型ー男
    public GameObject Woman;         //一般人型ー女
    private GameObject tempPeople;   //性別選択用

    private GameObject People=null;          //生成する町人を管理するため
    private int PeopleCount = 0;        //生成する町人の数、名前につける
    private float PeopleCreateSpeed=0; //町人生成のスピード
    [SerializeField, HeaderAttribute("最初の町人の生成時間")]
    public float firstCreateTime = 1f;  //最初の生成時間
    [SerializeField, HeaderAttribute("次の生成時間は以下の二つ値の間にランダム決定")]
    public float minCreateTime = 0.5f;   //生成時間最大値
    public float maxCreateTime = 2f;   //生成時間最小値

    public float SlowSpeedProb = 0.3f;//人が遅い速度になる確率(この数値より小さい場合)、デフォルト値
    public float FastSpeedProb = 0.8f;//人が早い速度になる確率(この数値より大きい場合)、デフォルト値
    //速度の調整はTimer.Scriptで時間ごとで調整しています

    //町人相関
    [HideInInspector]
    public static bool isPeopleCreate = true;  //テストのため、コントロールパネルで町人の出現を止める用   

    // Use this for initialization
    void Start () {
        PeopleDatasPass();
        MiddleZone = GameObject.Find("MiddleZone");
        Invoke("create_people",firstCreateTime);
    }
	
    void Update()
    {
        MaxX = MaxXTest;
        MaxY = MaxYTest;
        TopCreateVar = TopCreateVarTest;
    }
    void PeopleDatasPass()
    {
        firstCreateTime= GameObject.Find("GameController").GetComponent<LevelReader>().peoplefirstCreateTime;
        minCreateTime = GameObject.Find("GameController").GetComponent<LevelReader>().peopleminCreateTime;
        maxCreateTime = GameObject.Find("GameController").GetComponent<LevelReader>().peoplemaxCreateTime;
    }
    /// <summary>
    /// 町人の生成(amount/type/point/etc.)
    /// </summary>
    public void create_people()                                                //0=上から 　1=左から　 2=右から
    {
        //町人選択の追加
        int peopleChoose = Random.Range(0,2);
        switch (peopleChoose)
        {
            case 0:
                tempPeople = Man;
                break;
            case 1:
                tempPeople = Woman;
                break;
        }
        int i = Random.Range(0, 3);
        if (i == 0)      //上から　町人を生成
        {
            float x = Random.Range(MiddleZone.transform.position.x - TopCreateVar,
                MiddleZone.transform.position.x + TopCreateVar);
            PeopleCreatePoint = new Vector2(x, 0.5f);
            People = Instantiate(tempPeople, PeopleCreatePoint, Quaternion.identity);
            float j = Random.Range(0.0f, 1.0f); //お客さんになる確率と比べて、小さいだったら、お客さんになる
            bool BeCustomer = false;
            if (j < GameController.PofCustomers)               //お客さんになる
            {
                BeCustomer = true;
                customers_listadd("People" + PeopleCount);   //この町人のnameをリストに追加
            }
            People.SendMessage("enter_from_up", BeCustomer);
            People.name = "People" + PeopleCount;
            PeopleCount++;
        }
        else if (i == 1)   //左から生成
        {
            float y = Random.Range(MiddleZone.transform.position.y - MiddleZone.GetComponent<BoxCollider2D>().size.y / 2 + MiddleZone.GetComponent<BoxCollider2D>().offset.y,
                MiddleZone.transform.position.y + MiddleZone.GetComponent<BoxCollider2D>().size.y / -2 + MiddleZone.GetComponent<BoxCollider2D>().offset.y);
            PeopleCreatePoint = new Vector2(-MaxX, y);
            People = Instantiate(tempPeople, PeopleCreatePoint, Quaternion.identity);
            float j = Random.Range(0.0f, 1.0f); //お客さんになる確率と比べて、小さいだったら、お客さんになる
            bool BeCustomer = false;
            if (j < GameController.PofCustomers)               //お客さんになる
            {
                BeCustomer = true;
                customers_listadd("People" + PeopleCount);    //この町人のnameをリストに追加
            }
            People.SendMessage("enter_from_left", BeCustomer);
            People.name = "People" + PeopleCount;
            PeopleCount++;
        }
        else if (i == 2)  //右から生成
        {
            float y = Random.Range(MiddleZone.transform.position.y - MiddleZone.GetComponent<BoxCollider2D>().size.y / 2 + MiddleZone.GetComponent<BoxCollider2D>().offset.y,
                MiddleZone.transform.position.y + MiddleZone.GetComponent<BoxCollider2D>().size.y / 2+ MiddleZone.GetComponent<BoxCollider2D>().offset.y );
            PeopleCreatePoint = new Vector2(MaxX, y);
            People = Instantiate(tempPeople, PeopleCreatePoint, Quaternion.identity);
            float j = Random.Range(0.0f, 1.0f);//お客さんになる確率と比べて、小さいだったら、お客さんになる
            bool BeCustomer = false;
            if (j < GameController.PofCustomers)                //お客さんになる
            {
                BeCustomer = true;
                customers_listadd("People" + PeopleCount);   //この町人のnameをリストに追加
            }
            People.SendMessage("enter_from_right", BeCustomer);
            People.name = "People" + PeopleCount;
            PeopleCount++;
        }

        PeopleCreateSpeed = Random.Range(minCreateTime, maxCreateTime);       //生成スピードをランダムにする
        if(isPeopleCreate==true) {
            Invoke("create_people", PeopleCreateSpeed);
        }
        else
        {
            create_peoplenull();
        }

    }

    void create_peoplenull()
    {
        if (isPeopleCreate == false)
        {
            Invoke("create_peoplenull", 2f);
        }
        else
        {
            create_people();
        }

    }
    /// <summary>
    /// 待っているお客さんのリストを管理する
    /// </summary>
    /// <param name="ObjectName"></param>
    void customers_listadd(string ObjectName)
    {

        this.gameObject.GetComponent<GameController>().CustomerList.Add(ObjectName);
        People.SendMessage("customers_check2", (this.gameObject.GetComponent<GameController>().CustomerList.Count - 1));
    }
}
