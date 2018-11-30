using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect4Logic
{
    public class CellsManager
    {
        public event EventHandler<CellChangedEventArgs> OnCellChanged;

        public CellsManager()
        {
            InitialiseCells();
        }

        private void InitialiseCells()
        {
            Cells = new List<Cell>();

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Cells.Add(new Cell(new Position(i, j)));
                }
            }
        }

        public List<Cell> Cells { get; private set; }

        public bool TryInsertAtColumn(int column, Player homeOrAwayPlayer)
        {
            Cell emptycell = Cells.Where(x => x.Position.Column == column)
                                  .Reverse()
                                  .FirstOrDefault(x => x.OccupiedBy == Player.Nobody);

            if (emptycell != null)
            {
                emptycell.OccupiedBy = homeOrAwayPlayer;
                FireOnCellChanged(emptycell);
            }

            else return false;

            return true;
        }

        public bool CellsAreFull()
        {
            return !Cells.Any(x => x.OccupiedBy == Player.Nobody );
        }

        public bool CheckForWin(Cell lastCellPlayed)
        {
            Player played = lastCellPlayed.OccupiedBy;

            // look horizontally
            int counter = 1;

            // look right
            for (int i = 1; i < 4; i++)
            {
                if (Cells.Any(x => x.Position.Row == lastCellPlayed.Position.Row && x.Position.Column == lastCellPlayed.Position.Column + i && x.OccupiedBy == played))
                {
                    counter++;
                    continue;
                }

                break;
            }

            // look left
            for (int i = 1; i < 4; i++)
            {
                if (Cells.Any(x => x.Position.Row == lastCellPlayed.Position.Row && x.Position.Column == lastCellPlayed.Position.Column - i && x.OccupiedBy == played))
                {
                    counter++;
                    continue;
                }

                break;
            }

            if (counter > 3)
            {
                return true;
            }


            // look vertically
            counter = 1;

            // look down
            for (int i = 1; i < 4; i++)
            {
                if (Cells.Any(x => x.Position.Column == lastCellPlayed.Position.Column && x.Position.Row == lastCellPlayed.Position.Row + i && x.OccupiedBy == played))
                {
                    counter++;
                    continue;
                }

                break;
            }

            // look up
            for (int i = 1; i < 4; i++)
            {
                if (Cells.Any(x => x.Position.Column == lastCellPlayed.Position.Column && x.Position.Row == lastCellPlayed.Position.Row - i && x.OccupiedBy == played))
                {
                    counter++;
                    continue;
                }

                break;
            }

            if (counter > 3)
            {
                return true; ;
            }

            // look diagonally left up to right down
            counter = 1;

            // look left up
            for (int i = 1; i < 4; i++)
            {
                if (Cells.Any(x => x.Position.Row == lastCellPlayed.Position.Row - i && x.Position.Column == lastCellPlayed.Position.Column - i && x.OccupiedBy == played))
                {
                    counter++;
                    continue;
                }

                break;
            }

            // look right down
            for (int i = 1; i < 4; i++)
            {
                if (Cells.Any(x => x.Position.Row == lastCellPlayed.Position.Row + i && x.Position.Column == lastCellPlayed.Position.Column + i && x.OccupiedBy == played))
                {
                    counter++;
                    continue;
                }

                break;
            }

            if (counter > 3)
            {
                return true; ;
            }

            // look diagonally right up to left down
            counter = 1;

            // look right up
            for (int i = 1; i < 4; i++)
            {
                if (Cells.Any(x => x.Position.Row == lastCellPlayed.Position.Row - i && x.Position.Column == lastCellPlayed.Position.Column + i && x.OccupiedBy == played))
                {
                    counter++;
                    continue;
                }

                break;
            }

            // look left down
            for (int i = 1; i < 4; i++)
            {
                if (Cells.Any(x => x.Position.Row == lastCellPlayed.Position.Row + i && x.Position.Column == lastCellPlayed.Position.Column - i && x.OccupiedBy == played))
                {
                    counter++;
                    continue;
                }

                break;
            }

            if (counter > 3)
            {
                return true;
            }

            return false;
        }

        protected void FireOnCellChanged(Cell cell)
        {
            OnCellChanged?.Invoke(this, new CellChangedEventArgs(cell));
        }
    }
}
