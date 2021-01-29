using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.AI
{
    public class BoardSnapshot : SnapshotBase, ISnapshot
    {
        public MoveType?[,] Cells { get; }

        public BoardSnapshot(MoveType?[,] cells) : base()
        {
            this.Cells = (MoveType?[,])cells.Clone();
        }
    }
}
