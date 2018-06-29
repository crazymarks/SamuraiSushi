using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCreate : MonoBehaviour {
    public GameObject NinjaFly;
    public GameObject NinjaFlyDown;
    public GameObject NinjaWall;

    [SerializeField, HeaderAttribute("最初の忍者の生成時間")]
    public float firstCreateTime = 1f;  //最初の生成時間
    [SerializeField, HeaderAttribute("次の生成時間は以下の二つ値の間にランダム決定")]
    public float minCreateTime = 0.5f;   //生成時間最大値
    public float maxCreateTime = 2f;   //生成時間最小値

    [SerializeField, HeaderAttribute("忍者の出現確率だが、自分で百分比を計算 例：跳びは F/(W+F)")]
    public float probabilityNinjaFlyTest = 0.5f;  //飛び忍者の出現確率 テスト用
    public float probabilityNinjaWallTest = 0.5f;　　//壁忍者の出現確率　テスト用

    public static float probabilityNinjaFly = 0.5f;  //飛び忍者の出現確率
    public static float probabilityNinjaWall = 0.5f;　　//壁忍者の出現確率
　
    // Use this for initialization
    void Start () {
        Invoke("NinjaChoose",firstCreateTime);
    }
    /// <summary>
    /// テスト用　正式版消す
    /// </summary>
    void Update()
    {
        //テスト用　正式版消す
    　　 probabilityNinjaFly = probabilityNinjaFlyTest;  //飛び忍者の出現確率
    　　 probabilityNinjaWall = probabilityNinjaWallTest;  //壁忍者の出現確率
　　　　　//テスト用　正式版消す
}
        /// <summary>
        /// 忍者の種類を選択
        /// </summary>
        void NinjaChoose()
    {
        float ninjaCreate = Random.Range(0f, probabilityNinjaFly + probabilityNinjaWall);

        if (ninjaCreate > 0f && ninjaCreate < probabilityNinjaFly)
        {
            create_ninjafly();
        }
        else if (ninjaCreate < (probabilityNinjaFly + probabilityNinjaWall))
        {
            create_ninjaWall();
        }
        else
        {
            create_ninjanull();
        }
    }

    /// <summary>
    /// 飛び忍者を生成
    /// </summary>
    void create_ninjafly()
    {
        if (Random.value > 0.5)  //下から上に飛ぶ忍者
        {
            Vector3 pos = new Vector3(Random.Range(-7.0f, 7.0f), Random.Range(0.0f, 1.0f), 23);
            Instantiate(NinjaFly, pos, Quaternion.identity);
        }
        else　　　　　　　　　　　　　　//上から下に飛ぶ忍者
        {
            Vector3 pos = new Vector3(Random.Range(-7.0f, 7.0f), Random.Range(6.0f, 7.0f), 19);
            Instantiate(NinjaFlyDown, pos, Quaternion.identity);
        }

        float NinjaFlyCreateTime = Random.Range(minCreateTime, maxCreateTime);
        Invoke("NinjaChoose", NinjaFlyCreateTime);
    }




    /// <summary>
    /// 壁の忍者を生成
    /// </summary>
    void create_ninjaWall()
    {
        float a = Random.value;
        if (a < 0.5f)  //左の忍者生成
        {
            int leftNinjafly = Random.Range(0, 4);//左の忍者生成の所
            switch (leftNinjafly)
            {

                case 0:

                    Instantiate(NinjaWall, new Vector3(-4.5f, -2.5f, 15), Quaternion.identity);//左
                    break;//-4.5--2.5
                case 1:

                    Instantiate(NinjaWall, new Vector3(-4.7f, 0f, 15), Quaternion.identity);//上
                    break;//-4.7-0
                case 2:

                    Instantiate(NinjaWall, new Vector3(-2.6f, -0.2f, 15), Quaternion.identity);//中
                    break;//-2.6--0.2
                case 3:
                    Instantiate(NinjaWall, new Vector3(-1.3f, -1.2f, 15), Quaternion.identity);//右
                    break;//-1.3--1.2
                    
            }
        }
        else　　　　　　　　　　　　　　//右の忍者生成
        {
            int rightNinjafly = Random.Range(0, 4);//右側の忍者生成の所
            switch (rightNinjafly)
            {

                case 1:

                    Instantiate(NinjaWall, new Vector3(6f, -2.8f, 15), Quaternion.identity);//左下
                    break;//6--2.8
                case 2:

                    Instantiate(NinjaWall, new Vector3(5.3f, 0.1f, 15), Quaternion.identity);//右下
                    break;//5.3-0.1
                case 3:

                    Instantiate(NinjaWall, new Vector3(3.2f, -1.9f, 15), Quaternion.identity);//左上
                    break;//3.2--1.9
                case 0:
                    Instantiate(NinjaWall, new Vector3(1.2f, -1.2f, 15), Quaternion.identity);//右上
                    break;//1.2--1.2
            }

        }

        float NinjaFlyCreateTime = Random.Range(minCreateTime, maxCreateTime);
        Invoke("NinjaChoose", NinjaFlyCreateTime);
    }
    void create_ninjanull()  //テスト用、確率は全部０から戻す時、ちゃんと動くようにメソッド
    {
        float NinjaFlyCreateTime = Random.Range(minCreateTime, maxCreateTime);
        Invoke("NinjaChoose", NinjaFlyCreateTime);
    }

}
