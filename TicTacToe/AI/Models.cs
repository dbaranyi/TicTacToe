namespace TicTacToe.AI
{
    public enum MoveType
    {
        X, O
    }

    public struct GamePoint
    {
        private readonly int _x;
        private readonly int _y;

        public GamePoint(int x, int y) : this()
        {
            _x = x;
            _y = y;
        }

        public static GamePoint Parse(string point)
        {
            var coordinates = point.Split(';');
            int x = int.Parse(coordinates[0]);
            int y = int.Parse(coordinates[1]);
            return new GamePoint(x, y);
        }
        public override string ToString()
        {
            return string.Format("{0};{1}", X, Y);
        }

        public int X
        {
            get { return _x; }
        }
        public int Y
        {
            get { return _y; }
        }
    }

    public class GameLinePoint
    {
        public GameLinePoint() { }
        public GameLinePoint(GamePoint point, MoveType? moveType)
        {
            Point = point;
            MoveType = moveType;
        }

        public MoveType? MoveType { get; set; }
        public GamePoint Point { get; set; }
    }
}