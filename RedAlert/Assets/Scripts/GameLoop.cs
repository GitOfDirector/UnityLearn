using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    /*
     * 采用脚本和游戏物体分离的开发模式
     * 
     * 由该脚本调用其他游戏模块，除此脚本外其他脚本都不挂载物体
     */

    private SceneStateController controller;

    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start() 
    {
        controller = new SceneStateController();
        controller.SetState(new StartState(controller), false);
    }

    void Update() 
    {
        controller.UpdateState();
    }

}
