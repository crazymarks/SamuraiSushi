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
     float probabilityNinjaWall = 0f;　　//壁忍者の出現確率
　
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
        if (Random.value > 0.5)  //左の忍者生成
        {
            Vector3 pos = new Vector3(Random.Range(-7f, -3f), Random.Range(-1.0f, 1.0f), 15);
            Instantiate(NinjaWall, pos, Quaternion.identity);
        }
        else　　　　　　　　　　　　　　//右の忍者生成
        {
            Vector3 pos = new Vector3(Random.Range(3f, 7f), Random.Range(-1.0f, 1.0f), 15);
            Instantiate(NinjaWall, pos, Quaternion.identity);
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
