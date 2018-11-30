namespace Connect4Logic
{
    public class GameOverEventArgs
    {
        public GameOverEventArgs(Player winningPlayer)
        {
            WinningPlayer = winningPlayer;
        }

        public Player WinningPlayer { get; set; }

    }
}
