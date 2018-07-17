using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaFrame : MonoBehaviour

{

    public GameObject Frame;
    private float sa1;
    private float sa2;
    private float ScaleRate1;
    private float ScaleRate2;
    private void Start()
    {

        sa1 = transform.localScale.x ;//最初のサイズ
        sa2 = transform.localScale.y ;
        ScaleRate1 = sa1;//小さくする速度
        ScaleRate2 = sa2;
    }
    void Update()
    {
        ScaleRate1 = ScaleRate1 * 0.98f;//小さくきくする速度
        ScaleRate2 = ScaleRate2 * 0.98f;

        if (transform.localScale.x  >= sa1 * 0.2f  )//最初の0.2まで小さくする
        {
            transform.localScale = new Vector3(ScaleRate1, transform.localScale.y, 1);
            
        }
        if (transform.localScale.y >= sa2 * 0.2f)
        {
            transform.localScale = new Vector3(transform.localScale.x, ScaleRate2, 1);
        }
       
    }

}
