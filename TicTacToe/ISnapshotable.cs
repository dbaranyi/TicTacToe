using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.AI
{
    // 
    public interface ISnapshotable // https://fluidframework.com/apis/ordered-collection/isnapshotable/ ;)
    {
        ISnapshot Save();
        void Restore(ISnapshot s);
    }
}
