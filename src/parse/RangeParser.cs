using GameOfLife.extensions;
using GameOfLife.game;

namespace GameOfLife.parse;

public class RangeParser : Parser {
    public override bool TryParseExpression(string value, out ExpressionHandler result) {
        var strict = false;
        result = _ => false;

        if (!value.TryGetNumber(out var num)) {
            return false;
        }
        
        var start = Convert.ToInt32(num);
        var pointer = start.ToString().Length;

        if (!value.TryGetStreak('.', 2, out var separator, pointer)) {
            return false;
        }
        
        pointer += separator.Length;
        
        if (value.TryGetStreak('<', 1, out _, pointer)) {
            strict = true;
            pointer++;
        }
        
        if (!value.TryGetNumber(out num, pointer)) {
            return false;
        }
        
        var end = Convert.ToInt32(num);
        
        result = strict switch {
                     true  => input => input >= start && input < end,
                     false => input => input >= start && input <= end,
                 };
        
        return true;
    }
}