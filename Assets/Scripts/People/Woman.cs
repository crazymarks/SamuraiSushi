using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woman : PeopleBase {
    public GameObject WomanFrontUpperBody;
    public GameObject WomanFrontLowerBody;
    public Sprite front;
    public Sprite back;
    public Sprite side;//左向き
    public Sprite side2;//右向き
                        //!9/14　色かわりのため書いたもの　直した場所は　”//！9/14”で確認してください
    SpriteRenderer r; //SpriteRendererにセットする
    public GameObject Doku;
    //色のRGB
    float R = 255.0f;
    float G = 255.0f;
    float B = 255.0f;

    public bool bromatoxism = false;//变色开关



    // Use this for initialization
    void Start()
    {
        r = gameObject.GetComponent<SpriteRenderer>();   //SpriteRendererにセットする


    }

    // Update is called once per frame
    void Update()
    {

        //色の　R　　　　  G　　         B
        //　　　255-111    255-219       255 -117
        //      114        36            78
        //      22.8       7.2         　15.6
        //　　  0.76      　0.24 　　　    0.52 
        //!9/14修正
        //!*3   22.8        7.2        15.6
        //色々変わりの速さ
        if (bromatoxism == true)
        {
            if (R > 111.0f && G > 219.0f && B > 117.0f)
            {
                R = R - 11.4f;
                G = G - 3.5f;
                B = B - 7.8f;
                r.color = new Color(R / 255.0f, G / 255.0f, B / 255.0f, 255.0f / 255.0f);

            }

        }

    }



    public void after_eatsushi(int x)
    {
        IsCustomer = false;
        if (x == 3)　　//毒寿司なら死ぬ
        {//!-------------
            bromatoxism = true;//色変わり始める
            Doku = Instantiate(Doku, new Vector3(0.0f, -0.5f, 5.0f), Quaternion.identity);
            //！9/14
            Invoke("killed_by_poison", 1.5f);//1.5sまち

        }
        else
        {
            state_change();
        }
    }
    protected override void be_killed()
    {
        base.be_killed();
        GameObject tempObject1 = Instantiate(WomanFrontUpperBody, this.transform.position, this.transform.rotation);
        tempObject1.transform.localScale = this.transform.lossyScale;
        tempObject1.GetComponent<UpperBody>().state = GetSpriteState();
        GameObject tempObject2 = Instantiate(WomanFrontLowerBody, this.transform.position, this.transform.rotation);
        tempObject2.transform.localScale = this.transform.lossyScale;
        tempObject2.GetComponent<LowerBody>().state = GetSpriteState();
    }

    protected override void killed_by_poison()
    {
        GameObject.Find("SEPlayer").GetComponent<PlaySE>().KillWomanPoison();    //SE再生
        base.killed_by_poison();
    }


    protected override void SpriteInitalize()
    {
        base.SpriteInitalize();
        int state = GetSpriteState();
        switch (state)
        {
            case (int)MoveStatus.Front:
                this.GetComponent<SpriteRenderer>().sprite = front;   //正面に切り替え
                return;
            case (int)MoveStatus.Back:
                this.GetComponent<SpriteRenderer>().sprite = back;   //背面に切り替え

                return;
            case (int)MoveStatus.Left:
                this.GetComponent<SpriteRenderer>().sprite = side;   //左向きに切り替え
                return;
            case (int)MoveStatus.Right:
                this.GetComponent<SpriteRenderer>().sprite = side2;   //右向きに切り替え

                return;
        }

    }

    protected override void SpriteChange()
    {
        base.SpriteChange();
        int state = GetSpriteState();
        switch (state)
        {
            case (int)MoveStatus.Front:
                this.GetComponent<SpriteRenderer>().sprite = front;   //正面に切り替え
                return;
            case (int)MoveStatus.Back:
                this.GetComponent<SpriteRenderer>().sprite = back;   //背面に切り替え
                return;
            case (int)MoveStatus.Left:
                this.GetComponent<SpriteRenderer>().sprite = side;   //左向きに切り替え
                return;
            case (int)MoveStatus.Right:
                this.GetComponent<SpriteRenderer>().sprite = side2;   //右向きに切り替え
                return;
        }
    }
}
