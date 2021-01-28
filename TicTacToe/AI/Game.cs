namespace TicTacToe.AI
{
    public class Game : ISnapshotable
    {
        private readonly IGameBoard _gameBoard;
        private readonly IBoardAI _analyzer;
        private MoveType _currentMove;

        private bool CanMove(int x, int y)
        {
            if (!ValidateMove(x, y))
                return false;
            if (_gameBoard[x, y] != null)
                return false;
            return true;
        }
        private bool ValidateMove(int x, int y)
        {
            return (x >= 0 && x < _gameBoard.Size)
                && (y >= 0 && y < _gameBoard.Size);
        }

        public Game(IGameBoard gameBoard, IBoardAI analyzer, MoveType currentMove)
        {
            _gameBoard = gameBoard;
            _analyzer = analyzer;
            _currentMove = currentMove;
        }

        public bool CanMove(GamePoint point)
        {
            return CanMove(point.X, point.Y);
        }
        public void Move(GamePoint point)
        {
            if (CanMove(point))
            {
                _gameBoard[point.X, point.Y] = _currentMove;
                _currentMove = _currentMove == MoveType.X ? MoveType.O : MoveType.X;
            }
        }
        public MoveType? Get(GamePoint point)
        {
            return _gameBoard[point.X, point.Y];
        }
        public GameLinePoint[] WinnerLine()
        {
            return _analyzer.CheckWinnerLine(_gameBoard);
        }

        public int BoardSize
        {
            get { return _gameBoard.Size; }
        }

        public MoveType CurrentMove
        {
            get { return _currentMove; }
        }

        public ISnapshot Save()
        {
            ISnapshot boardSnapshot = _gameBoard.Save();

            return new GameSnapshot(this.BoardSize, this.CurrentMove, boardSnapshot);
        }
        public void Restore(ISnapshot s)
        {
            GameSnapshot snapshot = (GameSnapshot)s;
            
            this._currentMove = snapshot.CurrentMove;
            this._gameBoard.Restore(snapshot.BoardSnapshot);
        }
    }
}