using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaWallFrame : MonoBehaviour {

    public GameObject Frame;
    private float sa1;
    private float sa2;
    private float ScaleRate1;
    private float ScaleRate2;
    private void Start()
    {
        sa1 = transform.localScale.x * 2;
        sa2 = transform.localScale.y * 2;
        ScaleRate1 = sa1;
        ScaleRate2 = sa2;
    }
    void Update()
    {
        ScaleRate1 = ScaleRate1 * 0.99f;//ｘ大きくする速度
        ScaleRate2 = ScaleRate2 * 0.99f;

        if (transform.localScale.x <= sa1 *0.5f|| transform.localScale.y <= sa1 * 0.5f)
        {
            transform.localScale = new Vector3(ScaleRate1, ScaleRate2, 1);
            Debug.Log("suoxiao");
        }
    }

}
