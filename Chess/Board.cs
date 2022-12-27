using System;

namespace Chess
{
    internal class Board
    {
        public static Piece[] pieces = new Piece[0];
        public Board()
        {
            for (int y = 0; y <= 7; y += 7)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (x == 0 || x == 7) Add(new Rook(new Point(x, y), y == 0));
                    else if (x == 1 || x == 6) Add(new Knight(new Point(x, y), y == 0));
                    else if (x == 2 || x == 5) Add(new Bishop(new Point(x, y), y == 0));
                    else if (x == 3) Add(new Queen(new Point(x, y), y == 0));
                    else if (x == 4) Add(new King(new Point(x, y), y == 0));

                    if (y == 0) Add(new Pawn(new Point(x, y + 1), y == 0));
                    else if (y == 7) Add(new Pawn(new Point(x, y - 1), y == 0));
                }
            }

            Refresh();
        }
        public void Refresh()
        {
            Console.SetCursorPosition(0, 0);
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Console.BackgroundColor = new Point(x, y).Color;
                    Console.Write("  ");
                }
            }

            foreach (Piece piece in pieces)
            {
                Console.BackgroundColor = piece.Point.Color;
                Console.ForegroundColor = piece.Color;
                piece.Point.SetCursorPosition();
                Console.Write(piece);
            }
        }
        public void Add(Piece piece)
        {
            Array.Resize(ref pieces, pieces.Length + 1);
            pieces[pieces.Length - 1] = piece;
            Console.BackgroundColor = piece.Point.Color;
            Console.ForegroundColor = piece.Color;
            piece.Point.SetCursorPosition();
            Console.Write(piece);
        }
        public void Add(Point[] points)
        {
            if (points == null) return;
            foreach (Point point in points)
            {
                Console.BackgroundColor = point.Color;
                Console.ForegroundColor = ConsoleColor.Gray;
                point.SetCursorPosition();
                Console.Write("●");
            }
        }
    }
}
