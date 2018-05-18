using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaWall : MonoBehaviour {
    private float WaitTime = 0.3f;
    GameObject GameController;
    private float AttackTimeDelay = 0.0f;
    public GameObject AttackEffect;

    void Start()
    {
        GameController = GameObject.Find("GameController");
        AttackTimeDelay = Random.Range(3.0f, 5.0f);
    }

    //攻撃発動
    void attack()
    {
        GameController.SendMessage("get_hurt");
        AttackTimeDelay = Random.Range(2.0f, 4.0f);
        Vector3 pos = new Vector3(0, 0, 2);
        Instantiate(AttackEffect, pos, Quaternion.identity);
        Invoke("attack", AttackTimeDelay);
    }
    //切られた
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Line")
        {
            Invoke("die", WaitTime);
        }
    }
    void die()
    {
        Destroy(gameObject);
    }
}
