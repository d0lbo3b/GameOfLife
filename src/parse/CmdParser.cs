namespace GameOfLife.parse;

public class CmdParser : Parser {
    private readonly Dictionary<string, Action<string[], int>> _argumentsList;
    

    public CmdParser(string[] cmds, Action<string[], int>[] actions) {
        _argumentsList = new();

        for (var i = 0; i < cmds.Length; i++) {
            _argumentsList.Add(cmds[i], actions[i]);
        }
    }

    public override bool TryParse(string[] values) {
        var parseCounter = 0;
        
        for (var i = 0; i < values.Length; i++) {
            for (var j = 0; j < _argumentsList.Count; j++) {
                if (values[i] != _argumentsList.Keys.ElementAt(j)) {
                    continue;
                }

                _argumentsList.Values.ElementAt(j).Invoke(values, i);
                ++parseCounter;
            }
        }
        
        return parseCounter != 0;
    }
}