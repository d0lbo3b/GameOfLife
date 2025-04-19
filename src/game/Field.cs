using System.Numerics;
using GameOfLife.extensions;

namespace GameOfLife.game;

public class Field {
    private Cell[,] _grid;
    private Cell[,] _buffer;
    public Vector2 Paddings { get; }

    private bool _firstTimeDraw = true;
    private bool _firstLineDraw = true;
    
    public Vector2 GridSize { get; }
    
    
    public Field(Vector2 gridSize) {
        GridSize = new(gridSize.X, gridSize.Y);
        Paddings = new(0, 1);
        Initialize();
    }

    private void Initialize() {
        _grid = new Cell[(int)GridSize.X, (int)GridSize.Y];
        _buffer = new Cell[(int)GridSize.X, (int)GridSize.Y];
        
        for (var x = 0; x < GridSize.X; x++) {
            for (var y = 0; y < GridSize.Y; y++) {
                _grid[x, y] = new(x, y);
                _buffer[x, y] = new(x, y);
            }
        }
    }

    public void Update() {
        for (var x = 0; x < GridSize.X; x++) {
            for (var y = 0; y < GridSize.Y; y++) {
                if (!_firstTimeDraw && !IsGridEqualsToBuffer(x, y)) {
                    Console.SetCursorPosition(y*2+(int)(Paddings.X+Paddings.Y),x+1);
                    Console.Write($"\b{GetCell(x,y).GetSymbol()}");
                    continue;
                }
                
                if (!_firstTimeDraw) {
                    continue;
                }

                if (_firstLineDraw) {
                    Console.WriteLine();
                    _firstLineDraw = false;
                }
                    
                Console.Write($"{GetCell(x,y).GetSymbol()} ");
            }
            if (_firstTimeDraw) {
                Console.WriteLine();
            }
        }
        UpdateBuffer();
        _firstTimeDraw = false;
        
        Console.SetCursorPosition(0, 0);
    }

#region Getters

    public Vector2 GetLoopedPosition(int x, int y) {
        return new(x.Loop(0, (int)GridSize.X), y.Loop(0, (int)GridSize.Y));
    }
    
    public Cell GetCell(int x, int y) {
        return _grid[x, y];
    }
    
    public Cell GetCell(Vector2 pos) {
        return _grid[(int)pos.X, (int)pos.Y];
    }
    
    public Cell GetOutdatedCell(Vector2 pos) {
        return _buffer[(int)pos.X, (int)pos.Y];
    }

    public bool TryGetCell(Vector2 pos, out Cell cell) {
        if ((int)pos.X >= 0 && (int)pos.Y >= 0 && pos.X < GridSize.X && pos.Y < GridSize.Y) {
            cell = GetCell(pos);
            return true;
        }

        cell = null!;
        return false;
    }
    
    public bool TryGetCell(int x, int y, out Cell cell) {
        if (x >= 0 && y >= 0 && x < GridSize.X && y < GridSize.Y) {
            cell = GetCell(x, y);
            return true;
        }

        cell = null!;
        return false;
    }
    
    public bool TryGetOutdatedCell(int x, int y, out Cell cell) {
        if (x >= 0 && y >= 0 && x < GridSize.X && y < GridSize.Y) {
            cell = GetCell(x, y);
            return true;
        }

        cell = null!;
        return false;
    } 
    
    public bool TryGetOutdatedCell(Vector2 pos, out Cell cell) {
        if ((int)pos.X >= 0 && (int)pos.Y >= 0 && pos.X < GridSize.X && pos.Y < GridSize.Y) {
            cell = GetOutdatedCell(pos);
            return true;
        }

        cell = null!;
        return false;
    }
    
#endregion
    

    public void Paste(Cell[,] figure, Vector2 offset) {
        var size = new Vector2(figure.GetLength(0), figure.GetLength(1));

        for (var x = 0; x < size.X; x++) {
            for (var y = 0; y < size.Y; y++) {
                if (!TryGetCell(x+(int)offset.X, y+(int)offset.Y, out var cell)) continue;

                cell.State = figure[x, y].State;
            }
        }
    }
    
    private void UpdateBuffer() {
        for (var x = 0; x < GridSize.X; x++) {
            for (var y = 0; y < GridSize.Y; y++) {
                _buffer[x, y] = new(_grid[x, y]);
            }
        }
    }

    private bool IsGridEqualsToBuffer(int x, int y) {
        return _grid[x, y].State == _buffer[x, y].State;
    }
}