using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public abstract class BaseSnapshot
    {
        public DateTime TimeStamp { get; }

        public BaseSnapshot()
        {
            this.TimeStamp = DateTime.Now;
        }
    }
}
