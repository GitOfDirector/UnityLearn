using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BridgeMode : MonoBehaviour
{
    void Start() 
    {
        Shape s = new Sphere("Sphere", new DirectX());
        //s.Draw1();        
        s.Draw();
    }
}

public class Sphere :Shape
{
    public Sphere(string name, RenderEngine engine) :
        base(name, engine){}

    //public string name = "Sphere";

    //OpenGL gl = new OpenGL();

    //public void Draw1() 
    //{
    //    gl.Render(name);
    //}

    //public DirectX dx = new DirectX();

    //public void Draw2() 
    //{
    //    dx.Render(name);
    //}

}

public class OpenGL :RenderEngine
{
    //public void Render(string name) 
    //{
    //    Debug.Log("OpenGL 绘制了 " + name);
    //}

    public override void Render(string name)
    {
        Debug.Log("OpenGL 绘制了 " + name);
    }

}

public class DirectX : RenderEngine
{
    //public void Render(string name)
    //{
    //    Debug.Log("DirectX 绘制了 " + name);
    //}

    public override void Render(string name)
    {
        Debug.Log("DirectX 绘制了 " + name);
    }

}

public class Shape 
{
    string name;
    RenderEngine re;

    public Shape(string name, RenderEngine re)
    {
        this.name = name;
        this.re = re;
    }

    public void Draw() 
    {
        re.Render(name);
    } 

}

public abstract class RenderEngine 
{
    public abstract void Render(string name);
}











