using System;

namespace Chess
{
    internal class Point
    {
        private int x, y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public ConsoleColor Color
        {
            get
            {
                if (x % 2 == 0)
                {
                    if (y % 2 == 0) return ConsoleColor.Blue;
                    else return ConsoleColor.DarkBlue;
                }
                else
                {
                    if (y % 2 == 0) return ConsoleColor.DarkBlue;
                    else return ConsoleColor.Blue;
                }
            }
        }
        public Piece Piece
        {
            get
            {
                foreach (Piece piece in Board.pieces)
                {
                    if (x == piece.Point.X && y == piece.Point.Y) return piece;
                }
                return null;
            }
        }
        public void SetCursorPosition()
        {
            Console.SetCursorPosition(x * 2, y);
        }
    }
}
