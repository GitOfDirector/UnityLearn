using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SoldierChaseState : SoldierState
{

    public SoldierChaseState(SoldierFSMSystem fsm, Character cter)
        : base(fsm, cter)
    {
        mStateType = SoldierStateType.Chase;
    }

    public override void Reason(List<Character> targets)
    {
        if (targets == null)
            return;

        if (targets.Count == 0)
            return;

        float distance = Vector3.Distance(targets[0].Position, mCharacter.Position);
        if (distance <= mCharacter.AttackRange)
        {
            mFsm.PerformTransition(SoldierTransition.CanAttack);
        }
    }

    public override void Act(List<Character> targets)
    {
        if (targets != null && targets.Count != 0)
        {
            mCharacter.SetChaseTarget(targets[0].Position);

        }
    }
}
