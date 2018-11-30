using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4Logic
{
    public class CellChangedEventArgs : EventArgs
    {
        public CellChangedEventArgs(Cell changedCell)
        {
            ChangedCell = changedCell;
        }

        public Cell ChangedCell { get; set; }

    }
}
