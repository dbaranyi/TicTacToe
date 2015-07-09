using System.Windows;
using TicTacToe.Services;
using TicTacToe.ViewModel;

namespace TicTacToe.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameplayView : Window
    {
        public GameplayView()
        {
            InitializeComponent();
            DataContext = new GameplayViewModel(new DialogService { Owner = this });
        }
    }
}
