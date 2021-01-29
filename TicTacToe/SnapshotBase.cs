using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class SnapshotBase
    {
        public DateTime TimeStamp { get; }

        public SnapshotBase()
        {
            this.TimeStamp = DateTime.Now;
        }

    }
}
