using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IAttrStrategy
{
    /// <summary>
    /// 得到额外生命值
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    int GetExtraHPValue(int level);

    /// <summary>
    /// 减免的伤害值
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    int GetDamageDescValue(int level);

    /// <summary>
    /// 暴击伤害值
    /// </summary>
    /// <param name="critRate"></param>
    /// <returns></returns>
    float GetCritDamage(int critRate);

}