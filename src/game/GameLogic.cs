using GameOfLife.attributes;

namespace GameOfLife.game;

public enum GameLogicType {
    [GameLogic(typeof(OriginalGameLogic))]
    Original = 0,
}

public abstract class GameLogic {
    public virtual void Initialize(Field field) {}
    public virtual void Check() {}
}