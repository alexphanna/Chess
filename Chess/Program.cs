using System;

namespace Chess
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            Console.SetWindowSize(16, 8);
            Console.SetBufferSize(16, 8);
            Console.Title = "Chess";

            Board board = new Board();

            ConsoleKey key;
            Point cursor = new Point(0, 0);
            int position = 0;
            Point[] moves = new Point[0];
            while (true)
            {
                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (moves.Length > 0 && position < moves.Length - 1) cursor = moves[position++];
                        else if (moves.Length > 0 && position == moves.Length - 1) cursor = moves[0];
                        else if (moves.Length == 0)
                        {
                            if (cursor.Y == 0) cursor.Y = 7;
                            else cursor.Y--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (moves.Length > 0 && position > 0) cursor = moves[position--];
                        else if (moves.Length > 0 && position == 0) cursor = moves[moves.Length - 1];
                        else if (moves.Length == 0)
                        {
                            if (cursor.Y == 7) cursor.Y = 0;
                            else cursor.Y++;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (moves.Length > 0 && position < moves.Length - 1) cursor = moves[position++];
                        else if (moves.Length > 0 && position == moves.Length - 1) cursor = moves[0];
                        else if (moves.Length == 0)
                        {
                            if (cursor.X == 7) cursor.X = 0;
                            else cursor.X++;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (moves.Length > 0 && position > 0) cursor = moves[position--];
                        else if (moves.Length > 0 && position == 0) cursor = moves[moves.Length - 1];
                        else if (moves.Length == 0)
                        {
                            if (cursor.X == 0) cursor.X = 7;
                            else cursor.X--;
                        }
                        break;
                    case ConsoleKey.Enter:
                        moves = cursor.Piece.Moves;
                        cursor = moves[0];
                        break;
                    case ConsoleKey.Escape:
                        moves = new Point[0];
                        break;
                }

                board.Refresh();

                board.Add(moves);

                Console.BackgroundColor = ConsoleColor.DarkYellow;
                cursor.SetCursorPosition();
                if (cursor.Piece != null)
                {
                    Console.ForegroundColor = cursor.Piece.Color;
                    Console.Write(cursor.Piece);
                    board.Add(cursor.Piece.Moves);
                }
                else if (moves.Length > 0)
                {
                    Console.Write("●");
                }
                else Console.Write("  ");
            }
        }
    }
}
