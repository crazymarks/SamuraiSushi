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

    public void after_eatsushi(int x)
    {
        IsCustomer = false;
        if (x == 3)　　//毒寿司なら死ぬ
        {
            killed_by_poison();
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
