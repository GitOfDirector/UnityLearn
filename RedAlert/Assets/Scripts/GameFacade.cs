using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//外观模式  中介者模式
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

    private ArchievementSystem mArchievementSystem;
    private CampSystem mCampSystem;
    private CharacterSystem mCharacterSystem;
    private EnergySystem mEnergySystem;
    private GameEventSystem mGameEventSystem;
    private StageSystem mStageSystem;

    private CampInfoUI mCampInfoUI;
    private GamePassUI mGamePauseUI;
    private GameStateInfoUI mGameStateInfoUI;
    private SoldierInfoUI mSolierInfoUI;

    private GameFacade() { }

    public void Init() 
    {
        mArchievementSystem = new ArchievementSystem();
        mCampSystem = new CampSystem();
        mCharacterSystem = new CharacterSystem();
        mEnergySystem = new EnergySystem();
        mGameEventSystem = new GameEventSystem();
        mStageSystem = new StageSystem();

        mCampInfoUI = new CampInfoUI();
        mGamePauseUI = new GamePassUI();
        mGameStateInfoUI = new GameStateInfoUI();
        mSolierInfoUI = new SoldierInfoUI(); 

    }

    public void Update() 
    {
        mArchievementSystem.Update();
        mCampSystem.Update();
        mCharacterSystem.Update();
        mEnergySystem.Update();
        mGameEventSystem.Update();
        mStageSystem.Update();
        
        mCampInfoUI.Update();
        mGamePauseUI.Update();
        //mGameStateInfoUI.Update();
        mSolierInfoUI.Update();
    }

    public void Release() 
    {
        mArchievementSystem.Release();
        mCampSystem.Release();
        mCharacterSystem.Release();
        mEnergySystem .Release();
        mGameEventSystem.Release();
        mStageSystem.Release();
        
        mCampInfoUI.Release();
        mGamePauseUI.Release();
        //mGameStateInfoUI.Release();
        mSolierInfoUI.Release();
    }

}
