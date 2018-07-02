using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerBody : MonoBehaviour {
    private SpriteRenderer SR;
    private float transparency = 1.0f;
    int speedCount = 0;
    float speed = 0.25f;
    Vector3 targetPosition;
    [HideInInspector]
    public int state;
    public Sprite frontLower;
    public Sprite backLower;
    public Sprite sideLower;
    Quaternion rotation = Quaternion.identity;

    // Use this for initialization
    public enum MoveStatus  //町人の各移動状態　変数名は向き
    {
        Back = 1,
        Front = 2,
        Left = 3,
        Right = 4
    }
    void Start()
    {
        targetPosition = this.transform.position + new Vector3(-0.15f, -0.2f, 0f);
        SR = GetComponent<SpriteRenderer>();
        switch (state)
        {
            case (int)MoveStatus.Front:
                this.GetComponent<SpriteRenderer>().sprite = frontLower;   //正面に切り替え
                rotation.eulerAngles = new Vector3(0, 0, 0);
                this.transform.rotation = rotation;
                return;
            case (int)MoveStatus.Back:
                this.GetComponent<SpriteRenderer>().sprite = backLower;   //背面に切り替え
                rotation.eulerAngles = new Vector3(0, 0, 0);
                this.transform.rotation = rotation;
                return;
            case (int)MoveStatus.Left:
                this.GetComponent<SpriteRenderer>().sprite = sideLower;   //左向きに切り替え
                rotation.eulerAngles = new Vector3(0, 180, 0);
                this.transform.rotation = rotation;
                return;
            case (int)MoveStatus.Right:
                this.GetComponent<SpriteRenderer>().sprite = sideLower;   //右向きに切り替え
                rotation.eulerAngles = new Vector3(0, 0, 0);
                this.transform.rotation = rotation;
                return;
        }
    }

    private void Update()
    {
        speed = speedCount * -0.2f + 1.75f;
        if (speed < 0.15f)
        {
            speedCount = 8;
        }
        else { speedCount++; }
        float Step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Step);
        if (transform.position == targetPosition)
        {
            get_transparency();
        }
    }

    void get_transparency()
    {
        SR.color = new Vector4(SR.color.r, SR.color.g, SR.color.b, transparency);
        transparency -= 0.04f;

        Invoke("get_transparency", 0.05f);
        if (transparency <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
