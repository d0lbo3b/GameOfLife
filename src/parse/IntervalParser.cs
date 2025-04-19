using GameOfLife.extensions;
using GameOfLife.game;

namespace GameOfLife.parse;

public class IntervalParser : Parser {
    public override bool TryParseExpression(string value, out ExpressionHandler result) {
        result = _ => false;
        var pointer = 0;
        
        if (!value.TryGetStreak(["<", ">", "=", "<=", ">="], out var sign)) {
            return false;
        }

        pointer += sign.Length;

        if (!value.TryGetNumber(out var num, pointer)) {
            return false;
        }
        
        var number = Convert.ToInt32(num);

        result = sign switch {
                     "<"  => input => input < number,
                     ">"  => input => input > number,
                     "="  => input => input == number,
                     "<=" => input => input <= number,
                     ">=" => input => input >= number,
                     _    => result,
                 };
        
        return true;
    }
}