using GameOfLife.game;

namespace GameOfLife.parse;

public abstract class Parser {
    public virtual bool TryParse(string[] values) { return false; }
    
    public virtual bool TryParseExpression(string value, out ExpressionHandler result) {
        result = _ => false;
        return false; 
    }

    public virtual bool TryParseRule(string value, out Rule result) {
        result = null!;
        return false; 
    }
}