using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class StartState : SceneState
{
    private Image mLogo;


    public StartState(SceneStateController controller)
        : base("01-Start", controller)
    {

    }

    public override void StateStart()
    {
        mLogo = GameObject.Find("Canvas/Logo").GetComponent<Image>();
        mLogo.color = Color.black;
    }

    float t = 0;
    float smoothSpeed = 1;
    float waitTime = 2;

    public override void StateUpdate()
    {
        //第一种写法
        //if (t <= 1)
        //{
        //    t += Time.deltaTime;
        //    mLogo.color = Color.Lerp(Color.black, Color.white, t);
        //}

        //第二种写法
        mLogo.color = Color.Lerp(mLogo.color, Color.white, Time.deltaTime * smoothSpeed);

        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            mController.SetState(new MainMenuState(mController));
        }

    }
}
