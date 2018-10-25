using System;
using System.Collections.Generic;
using UnityEngine;

public class SoldierFSMSystem
{

    public SoldierState CurrentState
    {
        get { return mCurrentState; }
    }

    private List<SoldierState> mStates = new List<SoldierState>();

    private SoldierState mCurrentState;

    public void AddState(params SoldierState[] states) 
    {
        foreach (var item in states)
        {
            AddState(item);
        }
    }

    private void AddState(SoldierState state)
    {
        if (state == null)
        {
            Debug.Log("将要添加的状态为空");
        }
        if (mStates.Count == 0)
        {
            mStates.Add(state);
            mCurrentState = state;
            return;                
        }

        if (mStates.Contains(state))
        {
            Debug.Log("已经包含了该状态");
        }
        else
        {
            mStates.Add(state);
            mCurrentState = state;
        }

    }

    public void DeleteState(SoldierStateType stateType)
    {
        if (stateType == SoldierStateType.NullState)
        {
            Debug.Log("将要删除的状态为空");
        }
    }

    public void PerformTransition(SoldierTransition tran) 
    {
        if (tran == SoldierTransition.NullTransition)
        {
            Debug.Log("要执行的转换条件为空");
        }

        SoldierStateType nextState = mCurrentState.GetTargetStateType(tran);
        if (nextState == SoldierStateType.NullState)
        {
            Debug.Log("在 " + tran + " 条件下，没有对应的目标状态");
            return;
        }

        foreach (SoldierState item in mStates)
        {
            if (item.StateType == nextState)
            {
                mCurrentState.DoBeforeLeaving();
                mCurrentState = item;
                mCurrentState.DoBeforeEntering();
            }
        }

    }

}