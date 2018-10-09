using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuState :SceneState
{
    Button startBtn;

    public MainMenuState(SceneStateController controller)
        : base("02-MainMenu", controller)
    {
       
    }

    public override void StateStart()
    {
        startBtn = GameObject.Find("Canvas/StartGame").GetComponent<Button>();
        startBtn.onClick.AddListener(OnStartGameBtnClick);
    }

    private void OnStartGameBtnClick()
    {
        mController.SetState(new BattleSate(mController));
    }

    public override void StateEnd()
    {
        if (startBtn)
            startBtn.onClick.RemoveAllListeners();
    }

}
