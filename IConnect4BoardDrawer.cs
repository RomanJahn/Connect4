using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4Logic
{
    public interface IConnect4BoardDrawer
    {
        void DrawAllCellsAndBorder(List<Cell> listOfCells, bool preClearScreen);

        void ActualiseSingleCell(Cell cell);

        void ShowTextMessage(string message);
    }
}
