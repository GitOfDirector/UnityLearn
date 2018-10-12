using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public abstract class Character
{
    protected CharacterAttribute mCharacterAttr;

    protected GameObject mGameObject;
    protected NavMeshAgent mNavAgent;
    protected AudioSource mAudio;
    protected Weapon mWeapon;

    public Weapon weapon { set { mWeapon = value; } }

    public void Attack(Vector3 targetPos) 
    {
        mWeapon.Fire(targetPos);
    }

}
