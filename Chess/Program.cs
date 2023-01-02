using System;

namespace Chess
{
    internal class Program
    {
        public static Board board = null;
        public static int turn = 0;
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            Console.Title = "Chess";
            Console.SetWindowSize(16, 8);
            Console.SetBufferSize(16, 8);

            board = new Board();
            Point cursor = new Point(1, 1);
            Piece piece = null;
            Point[] moves = new Point[0];
            int index = 0;

            while (true)
            {
                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow when moves.Length == 0:
                        if (cursor.Y < 8) cursor.Y++;
                        else cursor.Y = 1;
                        break;
                    case ConsoleKey.DownArrow when moves.Length == 0:
                        if (cursor.Y > 1) cursor.Y--;
                        else cursor.Y = 8;
                        break;
                    case ConsoleKey.RightArrow when moves.Length == 0:
                        if (cursor.X < 8) cursor.X++;
                        else cursor.X = 1;
                        break;
                    case ConsoleKey.LeftArrow when moves.Length == 0:
                        if (cursor.X > 1) cursor.X--;
                        else cursor.X = 8;
                        break;
                    case ConsoleKey.DownArrow or ConsoleKey.LeftArrow when moves.Length > 0:
                        if (index > 0) index--;
                        else index = moves.Length - 1;
                        break;
                    case ConsoleKey.UpArrow or ConsoleKey.RightArrow when moves.Length > 0:
                        if (index < moves.Length - 1) index++;
                        else index = 0;
                        break;
                    case ConsoleKey.Enter when moves.Length == 0 && cursor.Piece != null:
                        piece = cursor.Piece; 
                        moves = cursor.Piece.CurrentMoves;
                        index = 0;
                        break;
                    case ConsoleKey.Enter when moves.Length > 0:
                        piece.Move(cursor);
                        goto case ConsoleKey.Escape;
                    case ConsoleKey.Escape:
                        moves = new Point[0];
                        board.Refresh();
                        break;
                }

                board.Refresh();

                if (moves.Length > 0)
                {
                    board.Add(moves);
                    cursor = moves[index];
                }

                Console.BackgroundColor = ConsoleColor.DarkYellow;
                cursor.SetCursorPosition();
                if (moves.Length == 0 && cursor.Piece != null)
                {
                    Console.ForegroundColor = cursor.Piece.Color.Item2;
                    Console.Write(cursor.Piece);
                    board.Add(cursor.Piece.CurrentMoves);
                }
                else if (moves.Length > 0 && cursor.Piece != null) Console.Write(cursor.Piece);
                else if (moves.Length > 0) Console.Write("●");
                else Console.Write("  ");
            }
        }
    }
}
