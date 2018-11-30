using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Connect4Logic
{
    public class Connect4Game
    {
        private bool gameOver;

        public event EventHandler<GameOverEventArgs> OnGameOver;

        public Connect4Game(IConnect4BoardDrawer drawer, Player startingPlayer)
        {
            CellsManager = new CellsManager();
            CellsManager.OnCellChanged += CellsManager_OnCellChanged;
            Drawer = drawer;
            PlayersTurn = startingPlayer;

            Drawer.DrawAllCellsAndBorder(CellsManager.Cells, true);
        }

        private void CellsManager_OnCellChanged(object sender, CellChangedEventArgs e)
        {
            Drawer.ActualiseSingleCell(e.ChangedCell);

            if (CellsManager.CheckForWin(e.ChangedCell))
            {
                gameOver = true;
                Drawer.ShowTextMessage($"Game Over! {e.ChangedCell.OccupiedBy} won the game!\nGame will be terminated in 3 seconds. Please wait...");
                Thread.Sleep(3000);
                FireOnGameOver(e.ChangedCell.OccupiedBy);
                return;
            }

            if (CellsManager.CellsAreFull())
            {
                gameOver = true;
                Drawer.ShowTextMessage($"Game Over! Nobody won the game! It's a draw!\nGame will be terminated in 3 seconds. Please wait...");
                Thread.Sleep(3000);
                FireOnGameOver(Player.Nobody);
                return;
            }
        }

        public CellsManager CellsManager { get; set; } //private field machen

        public Player PlayersTurn { get; set; } //private field machen

        public IConnect4BoardDrawer Drawer { get; set; } //private field machen

        public bool TryInsertStone(int column, Player player)
        {
            // check if correct player
            if (player == PlayersTurn)
            {
                // change players turn if insert was successfull
                if (CellsManager.TryInsertAtColumn(column - 1, player))
                {
                    //change players turn
                    PlayersTurn = (Player)((int)(PlayersTurn + 1) % 2);
                    return true;
                }
            }

            return false;
        }

        protected void FireOnGameOver(Player winningPlayer)
        {
            OnGameOver?.Invoke(this, new GameOverEventArgs(winningPlayer));
        }

    }
}
