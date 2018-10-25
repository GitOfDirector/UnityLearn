using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WeaponGun : Weapon
{



    protected override void PlayBulletEffect(Vector3 targetPos)
    {
        DoPlayBulletEffect(0.05f, 0.05f, targetPos);
    }

    protected override void PlaySound()
    {
        DoPlaySound();
    }

    protected override void SetEffectDisplayTime()
    {
        mEffectDisplayTime = 1.0f;
    }

}
