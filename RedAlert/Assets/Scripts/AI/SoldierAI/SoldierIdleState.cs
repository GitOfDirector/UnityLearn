using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SoldierIdleState : SoldierState
{
    public SoldierIdleState(SoldierFSMSystem fsm, Character cter)
        : base(fsm, cter)
    {
        mStateType = SoldierStateType.Idle;
    }

    public override void Reason(List<Character> targets)
    {
        
    }

    public override void Act(List<Character> targets)
    {
        mCharacter.PlayAnim("stand");
    }
}
