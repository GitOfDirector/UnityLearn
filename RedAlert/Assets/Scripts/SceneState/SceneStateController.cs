using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateController : MonoBehaviour
{
    private SceneState mState;
    private AsyncOperation asOpe;
    private bool isFinishStart;

    public void SetState(SceneState state, bool isLoadScene = true)
    {
        //结束当前状态
        if (mState != null)
            mState.StateEnd();

        mState = state;

        if (isLoadScene)
        {
            asOpe = SceneManager.LoadSceneAsync(mState.SceneName);
            isFinishStart = false;
        }
        else
        {
            //因为初始场景是默认加载的，无需切换
            mState.StateStart();
            isFinishStart = true;
        }
    }

    /// <summary>
    /// 状态的更新
    /// </summary>
    public void UpdateState()
    {
        //正在进行切换
        if (asOpe != null && !asOpe.isDone)
            return;

        //如果切换完毕
        if (asOpe != null && asOpe.isDone && !isFinishStart)
        {
            mState.StateStart();
            isFinishStart = true;
        }

        if (mState != null)
            mState.StateUpdate();
    }

}
