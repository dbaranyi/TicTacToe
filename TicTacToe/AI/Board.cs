using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.AI
{
    public interface IGameBoard
    {
        int Size { get; }
        MoveType? this[int row, int column] { get; set; }
        IEnumerable<IEnumerable<GameLinePoint>> SelectAllLines();
    }

    public class GameBoard : IGameBoard
    {
        private readonly MoveType?[,] _cells;

        public GameBoard(int size)
        {
            _cells = new MoveType?[size, size];
        }

        public IEnumerable<IEnumerable<GameLinePoint>> SelectAllLines()
        {
            var primeDiagonal = Enumerable.Range(0, Size)
                                          .Select(index => new GameLinePoint(new GamePoint(index, index), _cells[index, index]));
            var secondaryDiagonal = Enumerable.Range(0, Size)
                                              .Select(index => new GameLinePoint(new GamePoint(index, Size - 1 - index), _cells[index, Size - 1 - index]));
            var rows = Enumerable.Range(0, Size)
                                 .Select(row => Enumerable.Range(0, Size)
                                                          .Select(column => new GameLinePoint(new GamePoint(row, column), _cells[row, column])));
            var columns = Enumerable.Range(0, Size)
                                    .Select(column => Enumerable.Range(0, Size)
                                                                .Select(row => new GameLinePoint(new GamePoint(row, column), _cells[row, column])));
            return new[]{primeDiagonal, secondaryDiagonal}.Concat(rows).Concat(columns);
        }

        public MoveType? this[int row, int column]
        {
            get { return _cells[row, column]; }
            set { _cells[row, column] = value; }
        }
        public int Size
        {
            get { return _cells.GetLength(0); }
        }
    }
}