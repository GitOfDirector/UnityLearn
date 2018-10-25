using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SoldierAttackState : SoldierState
{

    private float mAttackRate = 1;
    private float attackTimer = 1;

    public SoldierAttackState(SoldierFSMSystem fsm, Character cter)
        : base(fsm, cter)
    {
        mStateType = SoldierStateType.Attack;
    }

    public override void Reason(List<Character> targets)
    {
        if (targets == null || targets.Count != 0)
        {
            mFsm.PerformTransition(SoldierTransition.NoEnemy);
            return;
        }

        float distance = Vector3.Distance(mCharacter.Position, targets[0].Position);

        if (distance <= mCharacter.AttackRange)
        {
            mFsm.PerformTransition(SoldierTransition.SeeEnemy);
        }

    }

    public override void Act(List<Character> targets)
    {
        if (targets != null && targets.Count != 0)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                mCharacter.Attack(targets[0].Position);
            }
        }
    }
}
