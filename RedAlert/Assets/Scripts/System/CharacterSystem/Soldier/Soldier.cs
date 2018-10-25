using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Soldier :Character
{
    protected SoldierFSMSystem mFSMSystem;

    public Soldier() :base()
    {

    }

    public void UpdateFSMAI(List<Character> targets) 
    {
        mFSMSystem.CurrentState.Reason(targets);
        mFSMSystem.CurrentState.Act(targets);
    }

    private void MakeFSM() 
    {
        mFSMSystem = new SoldierFSMSystem();

        SoldierIdleState idleState = new SoldierIdleState(mFSMSystem, this);
        idleState.AddTransition(SoldierTransition.SeeEnemy, SoldierStateType.Chase);


        SoldierChaseState chaseState = new SoldierChaseState(mFSMSystem, this);
        chaseState.AddTransition(SoldierTransition.NoEnemy, SoldierStateType.Idle);
        chaseState.AddTransition(SoldierTransition.CanAttack, SoldierStateType.Attack);


        SoldierAttackState attackState = new SoldierAttackState(mFSMSystem, this);
        attackState.AddTransition(SoldierTransition.NoEnemy, SoldierStateType.Idle);
        attackState.AddTransition(SoldierTransition.SeeEnemy, SoldierStateType.Chase);

        mFSMSystem.AddState(idleState, chaseState, attackState);
    
    }

}
