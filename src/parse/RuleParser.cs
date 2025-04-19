using GameOfLife.extensions;
using GameOfLife.game;

namespace GameOfLife.parse;

public class RuleParser : Parser {
    public override bool TryParseRule(string value, out Rule result) {
        result = null!;

        Parser parser;
        
        if (value.TryGetStreak(["<", ">", "=", "<=", ">="], out _)) {
            parser = new IntervalParser();
        } else if (value.TryFind("..")) {
            parser = new RangeParser();
        } else {
            return false;
        }

        if (!parser.TryParseExpression(value, out var expression)
         || !value.TryGetFirst(["a", "d"], out var initState, value.Length-2)
         || !value.TryGetFirst(["a", "d"], out var desiredState, value.Length-1)) {
            return false;
        }

        result =  new(
                      expression, 
                      initState == "a"? CellState.Alive : CellState.Dead,
                      desiredState == "a"? CellState.Alive : CellState.Dead
                     );
        
        return true;
    }
}