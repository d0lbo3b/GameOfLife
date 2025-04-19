namespace GameOfLife.game;

public class OriginalGameLogic : GameLogic {
    private readonly GameRules _rules;
    private Field _field;


    public OriginalGameLogic(GameRules rules) {
        _rules = rules;
    }

    public override void Initialize(Field field) {
        _field = field;
    }

    public override void Check() {
        for (var y = 0; y < _field.GridSize.Y; y++) {
            for (var x = 0; x < _field.GridSize.X; x++) {
                var cell = _field.GetCell(y, x);

                var liveNbours = cell.GetAliveNboursCount(_field);

                foreach (var rule in _rules.RuleSet.Where(rule => cell.State == rule.InitialState
                                                               && rule.Check.Invoke(liveNbours))) {
                    cell.State = rule.DesiredState;
                }
            }
        }
    }
}