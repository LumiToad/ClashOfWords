using UnityEngine;

public enum GameModeType
{
    None = 0,
    Score = 10,
    Time = 20,
    Free = 30,
    Golf = 40,
    MultiGolf = 50
}

public class GameModeBase : MonoBehaviour
{
    protected GameModeType gameModeType = GameModeType.None;
    public GameModeType GameModeType => gameModeType;

    public virtual void Setup(object data, GameModeType type, BaseView view)
    {
        gameModeType = type;
    }

    public virtual bool IsVictoryCondition(object data)
    {
        return false;
    }

    public virtual void GameEnd(object data)
    {

    }

    public virtual void GameEnd()
    {

    }

    public virtual void StageObjectInteraction(object item, object data)
    {

    }
}
