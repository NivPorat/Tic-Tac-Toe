using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{   /// <summary>
/// the type of value a cell in the game currently is
/// </summary>
    public enum MarkType
    {   /// <summary>
    /// the cell hasnt been clicked yet
    /// </summary>
        Free,
        /// <summary>
        /// the cell is O
        /// </summary>
        Circle,
        /// <summary>
        /// the cell is X
        /// </summary>
        Ex
    }
}
