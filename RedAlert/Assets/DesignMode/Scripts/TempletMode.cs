using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempletMode : MonoBehaviour
{
    void Start() 
    {
        HaveMeal nor = new NorthPeople();
        nor.Eat();
    }
    
}

public abstract class HaveMeal 
{
    public void Eat() 
    {
        //吃饭的三个步骤
        OrderFood();
        EatSomething();
        PayBill();
    }

    protected void OrderFood() 
    {
        Debug.Log("点餐");
    }

    protected virtual void EatSomething() { }

    protected void PayBill() 
    {
        Debug.Log("买单");
    }

}

public class NorthPeople :HaveMeal
{
    protected override void EatSomething()
    {
        Debug.Log("吃面条");
    }
}