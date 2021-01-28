using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.AI
{
    public class GameSnapshot : BaseSnapshot, ISnapshot
    {
        public ISnapshot BoardSnapshot { get; }
        public int Size { get; }
        public MoveType CurrentMove { get; }

        public GameSnapshot(int size, MoveType currentMove, ISnapshot boardSnapshot) : base()
        {
            this.Size = size;
            this.CurrentMove = currentMove;
            this.BoardSnapshot = boardSnapshot;
        }
    }
}
