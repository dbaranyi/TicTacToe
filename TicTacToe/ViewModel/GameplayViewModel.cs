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
        private bool _gameOver;
        private ObservableCollection<CellViewModel> _cells;
        private Game _game;

        private ObservableCollection<ISnapshot> _snapshots;

        private ISnapshot _selectedSnapshot;
        public ISnapshot SelectedSnapshot
        {
            get { return this._selectedSnapshot; }
            set { SetProperty(ref _selectedSnapshot, value); }
        }

        private bool OnCanCellPressed(string point)
        {
            var gamePoint = GamePoint.Parse(point);
            return !_gameOver && _game.CanMove(gamePoint);
        }
        private void OnCellPressed(string point)
        {
            var gamePoint = GamePoint.Parse(point);
            Move(gamePoint);
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
            _game = new Game(new GameBoard(BorderSize), new BoardAI(), MoveType.X);
	        _gameOver = false;
            Cells = new ObservableCollection<CellViewModel>(
                Enumerable.Range(0, ElementsOnBorderCount)
                          .Select(i => new CellViewModel(new RelayCommand<string>(OnCellPressed, OnCanCellPressed))));

            Snapshots = new ObservableCollection<ISnapshot>();
        }

        private void Move(GamePoint point)
        {
            _game.Move(point);
            Redraw();
            Snapshots.Add(_game.Save());
        }
        private void Redraw()
        {
            for (int i = 0; i < _game.BoardSize; i++)
            {
                for (int j = 0; j < _game.BoardSize; j++)
                {
                    GamePoint p = new GamePoint(i, j);
                    GetCell(p).MoveTypeOnCell = _game.Get(p);
                }
            }
            OnPropertyChanged(nameof(IsCurrentX));
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

            this.PropertyChanged += MyPropertyChanged;

            OnRetry();
        }

        private void MyPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectedSnapshot):
                    {
                        RestoreSnapshot(SelectedSnapshot);
                        break;
                    }
            }
        }

        public bool IsCurrentX
        {
            get { return _game.CurrentMove == MoveType.X; }
        }
        public ObservableCollection<CellViewModel> Cells
        {
            get { return _cells; }
            set { SetProperty(ref _cells, value); }
        }
        public ObservableCollection<ISnapshot> Snapshots
        {
            get { return _snapshots; }
            set { SetProperty(ref _snapshots, value); }
        }
        private void RestoreSnapshot(ISnapshot snapshot)
        {
            if (snapshot == null) return;
            _game.Restore(snapshot);
            Redraw();
        }

        public RelayCommand RetryCommand { get; private set; }
        public RelayCommand ExitCommand { get; private set; }
    }
}
