using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class StateMode
{
    void Start() 
    {
        Context context = new Context();
        context.SetState(new ConcreteStateA(context));

        context.HandleState(10);
        context.HandleState(5);
    }
}


/*
 * 状态模式
 * http://www.cnblogs.com/wangjq/archive/2012/07/16/2593485.html
 */

/// <summary>
/// 状态接口
/// </summary>
public interface IState
{
    void Handle(int arg);
}

/// <summary>
/// 当前上下文
/// </summary>
public class Context 
{
    private IState m_state;
    
    public void SetState(IState state) 
    {
        m_state = state;
    }

    public void HandleState(int arg) 
    {
        m_state.Handle(arg);
    }

}

/// <summary>
/// 处理状态A
/// </summary>
public class ConcreteStateA : IState 
{
    private Context m_Context;

    public ConcreteStateA(Context context) 
    {
        m_Context = context;
    }

    public void Handle(int arg)
    {
        Debug.Log("State A: " + arg);

        if (arg >= 10)
        {
            m_Context.SetState(new ConcreteStateB(m_Context));
        }
    }
}

/// <summary>
/// 处理状态B
/// </summary>
public class ConcreteStateB : IState
{
        private Context m_Context;

    public ConcreteStateB(Context context) 
    {
        m_Context = context;
    }

    public void Handle(int arg)
    {
        Debug.Log("State B: " + arg);

        if (arg <= 10)
        {
            m_Context.SetState(new ConcreteStateA(m_Context));
        }
    }
}

