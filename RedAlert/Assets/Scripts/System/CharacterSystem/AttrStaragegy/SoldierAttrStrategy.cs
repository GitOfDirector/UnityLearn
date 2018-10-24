using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SoldierAttrStrategy : IAttrStrategy
{

    public int GetExtraHPValue(int level)
    {
        return (level - 1) * 10;
    }

    public int GetDamageDescValue(int level)
    {
        return (level - 1) * 10;
    }

    public float GetCritDamage(int critRate)
    {
        return 0;
    }
}
