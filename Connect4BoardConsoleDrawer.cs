using Connect4Logic;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect4Network.ConsoleUI
{
    public class Connect4BoardConsoleDrawer : IConnect4BoardDrawer
    {
        //where to start drawing the cells in the console
        private readonly int topLeftCursorLeft;
        private readonly int topLeftCursorTop;

        //dimensions of cells and border
        private readonly int cellsWidth;
        private readonly int cellsHeight;
        private readonly int borderWidth;
        private readonly int borderHeight;

        //which character to use as border
        private readonly char borderChar;

        private int numberOfRows;
        private int numberOfColumns;

        private object locker;

        public Connect4BoardConsoleDrawer(IOptions<Connect4BoardConsoleDrawerOptions> options)
        {
            this.topLeftCursorLeft = options.Value.TopLeftCursorLeft;
            this.topLeftCursorTop = options.Value.TopLeftCursorTop;

            this.cellsWidth = options.Value.CellsWidth;
            this.cellsHeight = options.Value.CellsHeight;
            this.borderWidth = options.Value.BorderWidth;
            this.borderHeight = options.Value.BorderHeight;

            this.borderChar = options.Value.BorderChar;

            Console.CursorVisible = false;

            locker = new object();
        }

        public Connect4BoardConsoleDrawer(Connect4BoardConsoleDrawerOptions options)
        {
            this.topLeftCursorLeft = options.TopLeftCursorLeft;
            this.topLeftCursorTop = options.TopLeftCursorTop;

            this.cellsWidth = options.CellsWidth;
            this.cellsHeight = options.CellsHeight;
            this.borderWidth = options.BorderWidth;
            this.borderHeight = options.BorderHeight;

            this.borderChar = options.BorderChar;

            Console.CursorVisible = false;

            locker = new object();
        }

        public void ActualiseSingleCell(Cell cell)
        {
            if (cell == null) throw new ArgumentNullException(nameof(cell), "The specified argument must not be null!");

            lock (locker)
            {
                SetConsoleCursorPos(cell);

                switch (cell.OccupiedBy)
                {
                    case Player.Nobody:
                        DrawCell(ConsoleColor.White);
                        break;

                    case Player.You:
                        DrawCell(ConsoleColor.Yellow);
                        break;

                    case Player.Opponent:
                        DrawCell(ConsoleColor.Red);
                        break;
                }
            }
            
        }

        public void DrawAllCellsAndBorder(List<Cell> listOfCells, bool preClearScreen)
        {
            if (listOfCells == null) throw new ArgumentException("The specified argument must not be null!", nameof(listOfCells));

            lock (locker)
            {
                if (preClearScreen) Console.Clear();

                if (listOfCells == null || !listOfCells.Any()) throw new ArgumentNullException(nameof(listOfCells), "The specified argument must not be null or an empty list!");

                numberOfRows = listOfCells.Max(x => x.Position.Row) + 1;
                numberOfColumns = listOfCells.Max(x => x.Position.Column) + 1;

                Console.Clear();

                DrawBorder();

                foreach (Cell cell in listOfCells)
                {
                    ActualiseSingleCell(cell);
                }
            }
        }

        private void DrawCell(ConsoleColor color)
        {
            int topLeftPosLeft = Console.CursorLeft;
            int topLeftPosTop = Console.CursorTop;

            ConsoleColor initColor = Console.BackgroundColor;

            Console.BackgroundColor = color;

            for (int i = 0; i < cellsHeight; i++)
            {
                Console.SetCursorPosition(topLeftPosLeft, topLeftPosTop + i);

                for (int j = 0; j < cellsWidth; j++)
                {
                    Console.Write(" ");
                }
            }

            Console.BackgroundColor = initColor;
        }

        private void DrawBorder()
        {
            //print column numbers on top
            for (int i = 0; i < numberOfColumns; i++)
            {
                Console.SetCursorPosition(topLeftCursorLeft + i * (borderWidth + cellsWidth) + (borderWidth + cellsWidth) / 2 + 1, topLeftCursorTop);

                Console.Write(i + 1);
            }

            // draw horizontal lines
            for (int i = 1; i < numberOfRows + 1; i++)
            {
                Console.SetCursorPosition(topLeftCursorLeft, topLeftCursorTop + i * (borderHeight + cellsHeight));

                for (int j = 0; j < numberOfColumns * (borderWidth + cellsWidth) + borderWidth; j++)
                {
                    Console.Write(borderChar);
                }
            }

            // draw vertical lines
            for (int i = 0; i < numberOfColumns + 1; i++)
            {
                for (int j = 0; j < numberOfRows * (cellsHeight); j++)
                {
                    Console.SetCursorPosition(topLeftCursorLeft + i * (borderWidth + cellsWidth),
                                    topLeftCursorTop + borderHeight + j * (borderHeight + cellsHeight));

                    for (int k = 0; k < borderWidth; k++)
                    {
                        Console.Write(borderChar);
                    }
                }
            }

        }

        private void SetConsoleCursorPos(Cell cell)
        {
            Console.SetCursorPosition(
                    topLeftCursorLeft + borderWidth + cell.Position.Column * (cellsWidth + borderWidth),
                    topLeftCursorTop + borderHeight + cell.Position.Row * (cellsHeight + borderHeight));
        }

        public void ShowTextMessage(string message)
        {
            Console.SetCursorPosition(0, topLeftCursorTop + numberOfRows * (borderHeight + cellsHeight) + 2);

            ConsoleColor init = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(message);

            Console.ForegroundColor = init;
        }
    }
}
