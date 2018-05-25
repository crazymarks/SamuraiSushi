using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : PeopleBase {
    public GameObject ManFrontUpperBody;
    public GameObject ManFrontLowerBody;

    void after_eatsushi(int x)
    {
        IsCustomer = false;
        if (x == 3)　　//毒寿司なら死ぬ
        {
            killed_by_poison();
        }else
        {
            state_change();
        }
    }

    protected override void be_killed()
    {
        base.be_killed();
        GameObject tempObject1= Instantiate(ManFrontUpperBody,this.transform.position,Quaternion.identity);
        tempObject1.transform.localScale = this.transform.lossyScale;
        GameObject tempObject2= Instantiate(ManFrontLowerBody,this.transform.position,Quaternion.identity);
        tempObject2.transform.localScale = this.transform.lossyScale;
    }

}
