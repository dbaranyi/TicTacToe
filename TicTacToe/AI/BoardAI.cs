using System.Linq;

namespace TicTacToe.AI
{
    public interface IBoardAI
    {
        GameLinePoint[] CheckWinnerLine(IGameBoard gameBoard);
    }

    public class BoardAI : IBoardAI
    {
        private static bool CheckWinnerLine(GameLinePoint[] line)
        {
            MoveType?[] moves = line.Select(gameLine => gameLine.MoveType).Distinct().ToArray();
            return moves.Length == 1 && moves[0].HasValue;
        }

        public GameLinePoint[] CheckWinnerLine(IGameBoard gameBoard)
        {
            foreach (var line in gameBoard.SelectAllLines())
            {
                GameLinePoint[] testLine = line.ToArray();
                bool winner = CheckWinnerLine(testLine);
                if(winner)
                    return testLine;
            }
            return null;
        }
    }
}