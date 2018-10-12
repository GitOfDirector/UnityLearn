using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class Weapon
{
    protected int mAtk;
    protected float mAtkRange;
    protected int mAtkPlusValue;//暴击加成

    protected GameObject mGameObject;
    protected Character mOwner;

    protected ParticleSystem mParticle;
    protected LineRenderer mLine;
    protected Light mLight;
    protected AudioSource mAudio;


    public virtual void Fire(Vector3 targetPos)
    {
        Debug.Log("Fire!");
    }

}
