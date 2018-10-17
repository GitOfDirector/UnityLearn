using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyMode : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StrategyContext context = new StrategyContext();
        context.stategy = new ConcreteStrategyA();
        context.Cal();
    }


}

public class StrategyContext 
{
    public IStrategy stategy;

    public void Cal() 
    {
        stategy.Cal();
    }

}

public interface IStrategy 
{
    void Cal();
}

public class ConcreteStrategyA : IStrategy 
{

    public void Cal()
    {
        Debug.Log("使用A策略");
    }
}

public class ConcreteStrategyB : IStrategy
{

    public void Cal()
    {
        Debug.Log("使用B策略");
    }
}


