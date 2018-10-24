using System;
using UnityEngine;

public class EnemyAttrStrategy : IAttrStrategy
{
    public int GetExtraHPValue(int level)
    {
        return 0;
    }

    public int GetDamageDescValue(int level)
    {
        return 0;
    }

    public float GetCritDamage(int critRate)
    {
        float onceAttcackRate =  UnityEngine.Random.Range(0.0f, 100.0f);

        if (onceAttcackRate >= critRate)
        {
            float onceDamageTimes = UnityEngine.Random.Range(1.2f, 5.0f);
            return 10 * onceDamageTimes;
        }

        return 0;
    }
}