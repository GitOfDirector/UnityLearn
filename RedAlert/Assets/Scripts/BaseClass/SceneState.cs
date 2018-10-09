using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneState
{
    public string SceneName
    {
        get { return mSceneName; }
    }

    private string mSceneName;

    protected SceneStateController mController;
    
    public SceneState(string sceneName, SceneStateController controller) 
    {
        mSceneName = sceneName;
        mController = controller;
    }

    public virtual void StateStart() { }
    public virtual void StateEnd() { }
    public virtual void StateUpdate() { }

}
