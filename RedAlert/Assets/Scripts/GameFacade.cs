using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameFacade
{

    /*
     * 单例模式
     * http://blog.csdn.net/carson_ho/article/details/52223097
     * http://blog.csdn.net/iblade/article/details/51107308
     */

    public static GameFacade Instance 
    {
        get 
        {
            if (instance == null)
            {
                instance = new GameFacade();
            }

            return instance; 

        }
    }

    private static GameFacade instance;

    public bool IsGameOver
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }

    private bool isGameOver;

    private GameFacade() { }

    public void Init() 
    {

    }

    public void Update() 
    {

    }

    public void Release() 
    {

    }

}
