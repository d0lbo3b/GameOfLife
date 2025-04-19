namespace GameOfLife;

public static class CmdArgsHandler {
    public static readonly string[] arguments = [
                                                    "--help",
                                                    "--h",
                                                    "--s",
                                                    "--l",
                                                    "--r",
                                                    "--dt",
                                                ];

    public static readonly Action<string[], int>[] actions = [
                                                                 Help,
                                                                 Help,
                                                                 SetGridSize,
                                                                 SetGameLogic,
                                                                 SetGameRules,
                                                                 SetDeltaTime,
                                                             ];
    
    public delegate void NumberValueHandler(params int[] value);
    public delegate void StringValueHandler(params string[] value);
    
    public static event NumberValueHandler? OnSetGridSize;
    public static event NumberValueHandler? OnSetGameLogic;
    public static event NumberValueHandler? OnSetDeltaTime;
    public static event StringValueHandler? OnSetGameRules;
    
    
    private static void Help(string[] args, int index) {
        Console.WriteLine("Help: .\\GameOfLife <FLAG> [ARGS]                          \n"+
                          "\n    FLAGS:                                               \n"+
                          "    --help    arguments list                               \n"+
                          "    --h       arguments list                               \n"+
                          "    --s       grid size                                    \n"+
                          "    <> [width] [height]                                    \n" +
                          "    --l       game logic                                   \n"+
                          "    <> [index]                                             \n" +
                          "    --r       game rules                                   \n"+
                          "    <> [ranges or intervals]                               \n"+
                          "    --dt      frame time in ms                             \n"+
                          "    <> [ms]                                                \n" +
                          "\nRanges are written as a..b or a..<b                      \n"+
                          "Intervals are written as (=,<,>,<=,>=)a                    \n"+
                          "They mean the amount of cell nbours to execute the rule    \n"+
                          "\nYou also must define conditions for that rule:           \n"+
                          "Just after Range or Interval add 'a'(Alive) or 'd'(Dead)   \n"+
                          "\nDefault rules:                                           \n"+
                          "1.    <2ad      underpopulation                            \n"+
                          "2.    2..3aa    next generation                            \n"+
                          "3.    >3ad      overpopulation                             \n"+
                          "4.    =3da      reproduction                               \n");

        Console.ReadKey();
        Environment.Exit(0);
    }

    private static void SetGridSize(string[] args, int index) {
        OnSetGridSize?
           .Invoke(Convert.ToInt32(args[++index]), Convert.ToInt32(args[++index]));
    }

    private static void SetGameLogic(string[] args, int index) {
        OnSetGameLogic?
           .Invoke(Convert.ToInt32(args[++index]));
    }

    private static void SetGameRules(string[] args, int index) {
        var rules = new List<string>();

        for (var i = index; i < args.Length && arguments.Any(x => x != args[i]); i++) {
            rules.Add(args[i]);
        }
        
        OnSetGameRules?
           .Invoke(rules.ToArray());
    }

    private static void SetDeltaTime(string[] args, int index) {
        OnSetDeltaTime?.Invoke(Convert.ToInt32(args[++index]));
    }
}