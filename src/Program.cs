using System.Numerics;
using GameOfLife.attributes;
using GameOfLife.game;
using GameOfLife.parse;

namespace GameOfLife;

internal static class Program {
    private static Vector2 _fieldSize;
    
    private static GameLogic _gameLogic = null!;
    private static GameRules _gameRules = null!;

    private static int _deltaTime;
    

    private static void Main(string[] args) {
        Console.Clear();
        SetDefault();
        
        CmdArgsHandler.OnSetGridSize += SetGridSize;
        CmdArgsHandler.OnSetGameLogic += SetGameLogic;
        CmdArgsHandler.OnSetGameRules += SetGameRules;
        CmdArgsHandler.OnSetDeltaTime += SetDeltaTime;

        new CmdParser(CmdArgsHandler.arguments, CmdArgsHandler.actions)
           .TryParse(args);
        
        var field = new Field(_fieldSize);

        Console.SetWindowSize((int)_fieldSize.Y*2+(int)(field.Paddings.X+field.Paddings.Y), (int)_fieldSize.X+1);
    #pragma warning disable CA1416
        Console.SetBufferSize((int)_fieldSize.Y*2+(int)(field.Paddings.X+field.Paddings.Y), (int)_fieldSize.X+1);
    #pragma warning restore CA1416
        Console.CursorVisible = false;

        var glider = new Cell[,] { 
                                     {new(CellState.Dead),new(CellState.Dead),new(CellState.Alive),}, 
                                     {new(CellState.Alive),new(CellState.Dead),new(CellState.Alive),},
                                     {new(CellState.Dead),new(CellState.Alive),new(CellState.Alive),},
                                 };

        var blinker = new Cell[,] {
                                      {new(CellState.Dead), new(CellState.Dead), new(CellState.Dead),},
                                      {new(CellState.Alive), new(CellState.Alive), new(CellState.Alive),},
                                      {new(CellState.Dead), new(CellState.Dead), new(CellState.Dead),},
                                  };

        var block = new Cell[,] {
                                    {new(CellState.Alive), new(CellState.Alive),},
                                    {new(CellState.Alive), new(CellState.Alive),},
                                };

        var test = new Cell[,] {
                                   { new(CellState.Alive), new(CellState.Dead),new(CellState.Dead),},
                                   { new(CellState.Dead), new(CellState.Alive),new(CellState.Dead),},
                                   { new(CellState.Dead), new(CellState.Dead),new(CellState.Alive),},
                               };
        
        field.Paste(glider, new(15, 15));
        //field.Paste(blinker, new(15, 5));
        //field.Paste(block, new(10, 10));
        //field.Paste(test, new(0, 1));
        
        _gameLogic.Initialize(field);
        field.Update();
        
        while (true) {
            Thread.Sleep(_deltaTime);
            _gameLogic.Check();
            field.Update();
        }
    }

    private static void SetGridSize(int[] values) {
        if (values.Length < 2) {
            throw new("too few arguments");
        }
        
        _fieldSize = new(values[1], values[0]);
    }

    private static void SetGameLogic(int[] values) {
        var kind = (GameLogicType)values[0];

        var attr = AttributeUnwrapper.Unwrap<GameLogicAttribute, GameLogicType>(kind);
        if (attr == null) return;
        
        _gameLogic = ((GameLogic)Activator.CreateInstance(attr.Type)!);
    }

    private static void SetGameRules(string[] values) {
        var ruleSet = new List<Rule>();
        var parser = new RuleParser();
        
        for (var i = 1; i < values.Length && CmdArgsHandler.arguments.Any(x => x != values[i]); i++) {
            if (parser.TryParseRule(values[i++], out var range)) {
                ruleSet.Add(range);
            }
        }
        
        _gameRules.Set(ruleSet);
    }

    private static void SetDeltaTime(int[] values) {
        _deltaTime = values[0];
    }
    
    private static void SetDefault() {
        _deltaTime = 200;
        _fieldSize = new(20, 20);
        _gameRules = new();
        _gameLogic = new OriginalGameLogic(_gameRules);
    }
}