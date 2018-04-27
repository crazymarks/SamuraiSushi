using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCreate : MonoBehaviour {
    public GameObject NinjaFly;
    // Use this for initialization
    void Start () {
        Invoke("create_ninjafly", 5.0f);
    }

    /// <summary>
    /// 飛び忍者を生成
    /// </summary>
    void create_ninjafly()
    {
        Vector3 pos = new Vector3(Random.Range(-7f, 7f), Random.Range(2.0f, 3.0f), 23);
        Instantiate(NinjaFly, pos, Quaternion.identity);
        float NinjaFlyCreateTime = Random.Range(4.0f, 7.0f);
        Invoke("create_ninjafly", NinjaFlyCreateTime);
    }
}
