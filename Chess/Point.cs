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
                    if (y % 2 == 0) return ConsoleColor.DarkBlue;
                    else return ConsoleColor.Blue;
                }
                else
                {
                    if (y % 2 == 0) return ConsoleColor.Blue;
                    else return ConsoleColor.DarkBlue;
                }
            }
        }
        public bool Equals(Point point)
        {
            return point.X == X && point.Y == Y;
        }
        public void SetCursorPosition()
        {
            Console.SetCursorPosition((x - 1) * 2, 8 - y);
        }
    }
}
