using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4Logic
{
    public class Cell
    {
        public Cell(Position position)
        {
            Position = position;
            OccupiedBy = Player.Nobody;
        }

        public Position Position { get; set; }

        public Player OccupiedBy { get; set; }
    }
}
