using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BattleSate : SceneState
{
    public BattleSate(SceneStateController controller)
        : base("03-Battle", controller) 
    {

    }

    /*
     * 外观模式
     * http://blog.csdn.net/lovelion/article/details/8258121
     */

    //兵营 关卡 角色管理 行动力。。。子系统

    public override void StateStart()
    {
        GameFacade.Instance.Init();
    }
    public override void StateUpdate()
    {
        if (GameFacade.Instance.IsGameOver)
        {
            mController.SetState(new MainMenuState(mController));

            return;
        }

        GameFacade.Instance.Update();
    }

    public override void StateEnd()
    {

        GameFacade.Instance.Release();
    }
}
