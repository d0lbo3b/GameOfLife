namespace GameOfLife.game;

public class Rule {
    public ExpressionHandler Check { get; }
    public CellState InitialState { get; }
    public CellState DesiredState { get; }
    

    public Rule(ExpressionHandler check, CellState initState, CellState desiredState) {
        Check = check;
        InitialState = initState;
        DesiredState = desiredState;
    }

    public void Write() {
        Console.WriteLine($"{Check.Method}\n{InitialState}\n{DesiredState}");
    }
}