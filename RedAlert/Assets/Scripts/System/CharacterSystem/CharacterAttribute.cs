using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CharacterAttribute
{
    protected string mName;
    protected int mMaxHP;
    protected float mMoveSpeed;
    protected int mCurrentHP;
    protected string mIconSprite;

    protected int mLv;  //等级
    protected float mCritRate;  //0---1暴击几率


    //增加的血量，抵御的伤害值，暴击增加的伤害
    //采用不同的策略进行暴击或者等级提升的计算
    protected IAttrStrategy mStartegy;

}
