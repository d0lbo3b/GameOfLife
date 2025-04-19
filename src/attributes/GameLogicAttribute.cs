namespace GameOfLife.attributes;

public class GameLogicAttribute : Attribute {
    public Type Type { get; }


    public GameLogicAttribute() { }

    public GameLogicAttribute(Type type) {
        Type = type;
    }
}