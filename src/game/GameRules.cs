namespace GameOfLife.game;

public enum RuleType {
    Underpopulation = 0,
    NextGeneration = 1,
    Overpopulation = 2,
    Reproduction = 3,
}

public delegate bool ExpressionHandler(int value);

public class GameRules {
    public List<Rule> RuleSet { get; private set; }

    
    public GameRules() {
        RuleSet = [
                       new Rule(value => value < 2, CellState.Alive, CellState.Dead),
                       new Rule(value => value is 2 or 3, CellState.Alive, CellState.Alive),
                       new Rule(value => value > 3, CellState.Alive, CellState.Dead),
                       new Rule(value => value == 3, CellState.Dead, CellState.Alive),
                  ];
    }
    
    public GameRules(List<Rule> rules) {
        RuleSet = rules;
    }

    public void Set(List<Rule> rules) {
        RuleSet = rules;
    }
}