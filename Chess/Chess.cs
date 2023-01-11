using System;
using System.Diagnostics;

namespace Chess
{
    internal class Chess
    {
        public static Board board = new Board();
        public static int turn = 0;
        public static bool check = false;
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            Console.Title = "Chess";
            Console.SetWindowSize(16, 8);
            Console.SetBufferSize(16, 8);

            board;
            
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
                    case ConsoleKey.Enter when moves.Length == 0 && board.Exists(cursor) && board.Find(cursor).Color.Item1 == (turn % 2 == 0):
                        piece = board.Find(cursor); 
                        moves = board.Find(cursor).CurrentMoves;
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
                if (moves.Length == 0 && board.Exists(cursor))
                {
                    Console.ForegroundColor = board.Find(cursor).Color.Item2;
                    Console.Write(board.Find(cursor));
                    if (board.Find(cursor).Color.Item1 == (turn % 2 == 0)) board.Add(board.Find(cursor).CurrentMoves);
                }
                else if (moves.Length > 0 && board.Exists(cursor)) Console.Write(board.Find(cursor));
                else if (moves.Length > 0) Console.Write("●");
                else Console.Write("  ");
            }
        }
    }
}
