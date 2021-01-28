using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TicTacToe.AI;
using TicTacToe.Common;
using TicTacToe.Services;

namespace TicTacToe.ViewModel
{
    public class GameplayViewModel : ViewModelBase
    {
        public class CellViewModel : BindableBase
        {
            private bool _isWinner;
            private MoveType? _moveTypeOnCell;

            public CellViewModel(RelayCommand<string> cellPressedCommand)
            {
                CellPressedCommand = cellPressedCommand;
            }
            public CellViewModel(MoveType? moveTypeOnCell, bool isWinner, RelayCommand<string> cellPressedCommand) : this(cellPressedCommand)
            {
                _moveTypeOnCell = moveTypeOnCell;
                _isWinner = isWinner;
            }

            public MoveType? MoveTypeOnCell
            {
                get { return _moveTypeOnCell; }
                set { SetProperty(ref _moveTypeOnCell, value); }
            }
            public bool IsWinner
            {
                get { return _isWinner; }
                set { SetProperty(ref _isWinner, value); }
            }
            
            public RelayCommand<string> CellPressedCommand { get; private set; } 
        }

        private const int BorderSize = 3;
        private const int ElementsOnBorderCount = BorderSize*BorderSize;

        private readonly IDialogService _dialogService;
        private bool _isCurrentX;
        private bool _gameOver;
        private ObservableCollection<CellViewModel> _cells;
        private Game _game;

        private bool OnCanCellPressed(string point)
        {
            var gamePoint = GamePoint.Parse(point);
            return !_gameOver && _game.CanMove(gamePoint);
        }
        private void OnCellPressed(string point)
        {
            var gamePoint = GamePoint.Parse(point);
            Move(gamePoint, _isCurrentX ? MoveType.X : MoveType.O);
            IsCurrentX = !IsCurrentX;
            var winnerLine = _game.WinnerLine();
            if (winnerLine != null && winnerLine.Length > 0)
            {
                foreach (GameLinePoint linePoint in winnerLine)
                    GetCell(linePoint.Point).IsWinner = true;
                var winner = winnerLine[0].MoveType;
                _dialogService.Show(string.Format("{0} WIN!", winner));
		        _gameOver = true;
            }
        }
        private void OnExit()
        {
            Application.Current.Shutdown();
        }
        private void OnRetry()
        {
            _game = new Game(new GameBoard(BorderSize), new BoardAI());
	        _gameOver = false;
            IsCurrentX = true;
            Cells = new ObservableCollection<CellViewModel>(
                Enumerable.Range(0, ElementsOnBorderCount)
                          .Select(i => new CellViewModel(new RelayCommand<string>(OnCellPressed, OnCanCellPressed))));
        }

        private void Move(GamePoint point, MoveType moveType)
        {
            _game.Move(point, moveType);
            GetCell(point).MoveTypeOnCell = moveType;
        }
        private CellViewModel GetCell(GamePoint point)
        {
            return _cells[point.X * _game.BoardSize + point.Y];
        }

        public GameplayViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            RetryCommand = new RelayCommand(OnRetry);
            ExitCommand =  new RelayCommand(OnExit);
            OnRetry();
        }

        public bool IsCurrentX
        {
            get { return _isCurrentX; }
            set { SetProperty(ref _isCurrentX, value); }
        }
        public ObservableCollection<CellViewModel> Cells
        {
            get { return _cells; }
            set { SetProperty(ref _cells, value); }
        }

        public RelayCommand RetryCommand { get; private set; }
        public RelayCommand ExitCommand { get; private set; }
    }
}
