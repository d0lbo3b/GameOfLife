using System.Numerics;

namespace GameOfLife.game;

public enum CellState {
    Dead = '.',
    Alive = '■',
}

public class Cell {
    public CellState State { get; set; }

    public Vector2 Position { get; private set; }
    
    
    public Cell(int x, int y) {
        State = CellState.Dead;
        Position = new(x, y);
    }
    
    public Cell(int x, int y, CellState state) {
        State = state;
        Position = new(x, y);
    }
    
    public Cell() {
        State = CellState.Dead;
        Position = new(0 ,0);
    }
    
    public Cell(CellState state) {
        State = state;
        Position = new(0 ,0);
    }

    public Cell(Cell cell) {
        State = cell.State;
        Position = cell.Position;
    }

    public char GetSymbol() {
        return (char)State;
    }

    public void ChangeState() {
        State = State == CellState.Alive ? CellState.Dead : CellState.Alive;
    }
    
    public int GetAliveNboursCount(Field field) {
        return GetNbours(field).Sum(nbour => nbour.State == CellState.Alive ? 1 : 0);
    }

    public Cell[] GetNbours(Field field) {
        var result = new List<Cell>();
        
        for (var dx = -1; dx <= 1; dx++) {
            for (var dy = -1; dy <= 1; dy++) {
                if (field.TryGetOutdatedCell(field.GetLoopedPosition((int)Position.X+dx, (int)Position.Y+dy), out var cell)
                    && cell.Position != Position) {
                    result.Add(cell);
                }
            }
        }
        return result.ToArray();
    }
}