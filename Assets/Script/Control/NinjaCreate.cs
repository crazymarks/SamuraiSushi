using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCreate : MonoBehaviour {
    public GameObject NinjaFly;
    public GameObject NinjaFlyDown;
    public GameObject NinjaWall;

    [HideInInspector]
    public float probabilityNinjaFly = 0.5f;  //飛び忍者の出現確率
    [HideInInspector]　
    public float probabilityNinjaWall = 0.5f;　　//壁忍者の出現確率
　
    // Use this for initialization
    void Start () {
       float ninjaCreate = Random.Range(0f, probabilityNinjaFly+probabilityNinjaWall);

        if (ninjaCreate>0f&&ninjaCreate<probabilityNinjaFly)
        {
            create_ninjafly();
        }else if (ninjaCreate < (probabilityNinjaFly + probabilityNinjaWall))
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

        float NinjaFlyCreateTime = Random.Range(4.0f, 7.0f);
        Invoke("Start", NinjaFlyCreateTime);
    }




    /// <summary>
    /// 壁の忍者を生成
    /// </summary>
    void create_ninjaWall()
    {
        float a = Random.value;
        Debug.Log(a);
        if (a < 0.5f)  //左の忍者生成
        {
            int leftNinjafly = Random.Range(0, 4);//左の忍者生成の所
            switch (leftNinjafly)
            {

                case 0:

                    Instantiate(NinjaWall, new Vector3(-6.6f, -1.3f, 15), Quaternion.identity);//左
                    break;
                case 1:

                    Instantiate(NinjaWall, new Vector3(-4.95f, -0.6f, 15), Quaternion.identity);//上
                    break;
                case 2:

                    Instantiate(NinjaWall, new Vector3(-4.9f, 1.6f, 15), Quaternion.identity);//中
                    break;
                case 3:
                    Instantiate(NinjaWall, new Vector3(-2.1f, -0.4f, 15), Quaternion.identity);//右
                    break;



            }
        }
        else　　　　　　　　　　　　　　//右の忍者生成
        {
            int rightNinjafly = Random.Range(0, 4);//右側の忍者生成の所
            switch (rightNinjafly)
            {

                case 1:

                    Instantiate(NinjaWall, new Vector3(3.1f, 0.3f, 15), Quaternion.identity);//左下
                    break;
                case 2:

                    Instantiate(NinjaWall, new Vector3(6.05f, -1.14f, 15), Quaternion.identity);//右下
                    break;
                case 3:

                    Instantiate(NinjaWall, new Vector3(4.54f, 1.6f, 15), Quaternion.identity);//左上
                    break;
                case 0:
                    Instantiate(NinjaWall, new Vector3(7.14f, 1.64f, 15), Quaternion.identity);//右上
                    break;
            }

        }

        float NinjaFlyCreateTime = Random.Range(4.0f, 7.0f);
        Invoke("Start", NinjaFlyCreateTime);
    }
    void create_ninjanull()  //テスト用、確率は全部０から戻す時、ちゃんと動くようにメソッド
    {
        float NinjaFlyCreateTime = Random.Range(4.0f, 7.0f);
        Invoke("Start", NinjaFlyCreateTime);
    }

}
