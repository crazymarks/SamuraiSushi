using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCreate : MonoBehaviour {
    public GameObject NinjaFly;
    public GameObject NinjaWall1;
    int ninjaCreate = 0;
    // Use this for initialization
    void Start () {
        ninjaCreate = Random.Range(1, 3);
        switch (ninjaCreate)
        {
            case 1:
                create_ninjafly();
                break;
            case 2:
                create_ninjaWallLeft();
                break;
            case 3:
                create_ninjaWallRight();
                break;
        }
      

    }

    /// <summary>
    /// 飛び忍者を生成
    /// </summary>
    void create_ninjafly()
    {
        Vector3 pos = new Vector3(Random.Range(-7f, 7f), Random.Range(2.0f, 3.0f), 23);
        Instantiate(NinjaFly, pos, Quaternion.identity);
        float NinjaFlyCreateTime = Random.Range(4.0f, 7.0f);
        Invoke("Start", 4.0f);
    }

    /// <summary>
    /// 左側壁の忍者を生成
    /// </summary>
    void create_ninjaWallLeft()
    {
        Vector3 pos = new Vector3(Random.Range(-7f, -3f), Random.Range(-1.0f, 1.0f), 15);
        Instantiate(NinjaWall1, pos, Quaternion.identity);
        float NinjaFlyCreateTime = Random.Range(4.0f, 7.0f);
        Invoke("Start", 4.0f);
    }

    /// 右側壁の忍者を生成
    /// </summary>
    void create_ninjaWallRight()
    {
        Vector3 pos = new Vector3(Random.Range(3f, 7f), Random.Range(-1.0f, 1.0f), 15);
        Instantiate(NinjaWall1, pos, Quaternion.identity);
        float NinjaFlyCreateTime = Random.Range(4.0f, 7.0f);
        Invoke("Start", 4.0f);
    }
}
