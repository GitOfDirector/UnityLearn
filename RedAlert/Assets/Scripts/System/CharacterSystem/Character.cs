using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public abstract class Character
{
    public Weapon Weapon { set { mWeapon = value; } }

    public Vector3 Position
    {
        get
        {
            if (mGameObject == null)
            {
                Debug.Log("mGameObject为空");
                return Vector3.zero;
            }

            return mGameObject.transform.position;
        }
    }

    public float AttackRange 
    {
        get { return mWeapon.AtkRange; }
    }

    protected CharacterAttribute mCharacterAttr;

    protected GameObject mGameObject;
    protected NavMeshAgent mNavAgent;
    protected AudioSource mAudio;
    protected Weapon mWeapon;
    protected Animation mAnim;

    public void Attack(Vector3 targetPos) 
    {
        mWeapon.Fire(targetPos);
    }

    public void SetChaseTarget(Vector3 targetPos) 
    {
        mNavAgent.SetDestination(targetPos);
    }

    public void PlayAnim(string ani) 
    {
        mAnim.Play(ani);
    }

}
