using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : PeopleBase {
    void after_eatsushi(int x)
    {
        IsCustomer = false;
        if (x == 3)
        {
            killed_by_poison();
        }else
        {
            state_change();
        }
    }
}
