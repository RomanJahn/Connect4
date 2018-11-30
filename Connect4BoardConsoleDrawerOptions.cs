using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4Network.ConsoleUI
{
    public class Connect4BoardConsoleDrawerOptions
    {
        public int TopLeftCursorLeft { get; set; }

        public int TopLeftCursorTop { get; set; }

        public int CellsWidth { get; set; }

        public int CellsHeight { get; set; }

        public int BorderWidth { get; set; }

        public int BorderHeight { get; set; }

        public char BorderChar { get; set; }
    }
}
