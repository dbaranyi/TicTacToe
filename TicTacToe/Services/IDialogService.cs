namespace TicTacToe.Services
{
    public enum DialogServiceResult
    {
        None,
        OK,
        Cancel,
        Abort,
        Retry,
        Ignore,
        Yes,
        No
    }

    public interface IDialogService
    {
        DialogServiceResult Show(string messageText);
        DialogServiceResult Show(string messageText, string caption);
    }
}
