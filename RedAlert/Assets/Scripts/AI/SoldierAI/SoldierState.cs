using System;
using System.Collections.Generic;
using UnityEngine;

public enum SoldierTransition
{
    NullTransition,
    SeeEnemy,
    NoEnemy,
    CanAttack,
}

public enum SoldierStateType
{
    NullState,
    Idle,
    Chase,
    Attack,
}

public abstract  class SoldierState
{
    public SoldierStateType StateType { get { return mStateType; } }

    protected Dictionary<SoldierTransition, SoldierStateType> relationDic = new Dictionary<SoldierTransition, SoldierStateType>();

    protected SoldierStateType mStateType;
    protected Character mCharacter;
    protected SoldierFSMSystem mFsm;

    public SoldierState(SoldierFSMSystem fsm, Character cter)
    {
        mFsm = fsm;
        mCharacter = cter;
    }

    public void AddTransition(SoldierTransition tran, SoldierStateType state)
    {
        if (tran == SoldierTransition.NullTransition)
        {
            Debug.Log("转换条件为空");
        }

        if (state == SoldierStateType.NullState)
        {
            Debug.Log("目标状态为空");
        }

        if (relationDic.ContainsKey(tran))
        {
            Debug.Log("已包含该转换");
        }
        relationDic.Add(tran, state);

    }

    public void DeleteTransition(SoldierTransition tran) 
    {
        if (!relationDic.ContainsKey(tran))
        {
            Debug.Log("不包含该转换：" + tran);
            return;
        }

        relationDic.Remove(tran);
    }

    /// <summary>
    /// 得到指定条件的目标状态
    /// </summary>
    /// <param name="tran"></param>
    /// <returns></returns>
    public SoldierStateType GetTargetStateType(SoldierTransition tran) 
    {
        if (!relationDic.ContainsKey(tran))
        {
            return SoldierStateType.NullState;
        }
        else
        {
            return relationDic[tran];
        }
    }

    public virtual void DoBeforeEntering() { }
    public virtual void DoBeforeLeaving() { }

    public abstract void Reason(List<Character> targets);


    public abstract void Act(List<Character> targets);

}
