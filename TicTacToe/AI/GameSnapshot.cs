using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.AI
{
    public class GameSnapshot : SnapshotBase, ISnapshot
    {
        public ISnapshot BoardSnapshot { get; }

        public MoveType CurrentMove { get; }

        public GameSnapshot(MoveType currentMove, ISnapshot boardSnapshot) : base()
        {
            this.CurrentMove = currentMove;
            this.BoardSnapshot = boardSnapshot;
        }
    }
}
