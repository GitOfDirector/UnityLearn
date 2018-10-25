using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class Weapon
{
    public float AtkRange
    {
        get { return mAtkRange; }
    }

    protected int mAtk;
    private float mAtkRange;

    protected int mAtkPlusValue;//暴击加成

    protected GameObject mGameObject;
    protected Character mOwner;

    protected ParticleSystem mParticle;
    protected LineRenderer mLine;
    protected Light mLight;
    protected AudioSource mAudio;

    protected float mEffectDisplayTime;

    public void Update() 
    
    {
        if (mEffectDisplayTime >= 0)
        {
            mEffectDisplayTime -= Time.deltaTime;
            if (mEffectDisplayTime <= 0)
            {
                DisableEffect();
            }
        }
    }

    public virtual void Fire(Vector3 targetPos)
    {
        Debug.Log("Fire!");

        //显示枪口特效
        PlayMuzzleEffect();

        //显示子弹轨迹
        PlayBulletEffect(targetPos);

        //设置特效显示时间
        SetEffectDisplayTime();

        //播放声音
        PlaySound();

    }

    protected virtual void PlayMuzzleEffect() 
    {
        mParticle.Stop();
        mParticle.Play();
        mLight.enabled = true;
    }

    protected abstract void SetEffectDisplayTime();

    protected abstract void PlayBulletEffect(Vector3 targetPos);

    protected void DoPlayBulletEffect(float startWidth, float endWidth, Vector3 targetPos) 
    {
        mLine.enabled = true;
        mLine.startWidth = startWidth;
        mLine.endWidth = endWidth;
        mLine.SetPosition(0, mGameObject.transform.position);
        mLine.SetPosition(1, targetPos);
    }

    protected abstract void PlaySound();

    protected void DoPlaySound() 
    {
        string clipName = "GunShot";
        AudioClip clip = null;//TODO
        mAudio.clip = clip;
        mAudio.Play();
    }

    private void DisableEffect() 
    {
        mLight.enabled = false;
        mLine.enabled = false;
    }

}
