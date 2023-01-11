using System;

namespace Chess
{
    internal class Board
    {
        private Piece[] pieces = new Piece[0];
        public Board()
        {
            for (int y = 1; y <= 8; y += 7)
            {
                for (int x = 1; x <= 8; x++)
                {
                    if (x == 1 || x == 8) Add(new Rook(new Point(x, y), y == 1));
                    else if (x == 2 || x == 7) Add(new Knight(new Point(x, y), y == 1));
                    else if (x == 3 || x == 6) Add(new Bishop(new Point(x, y), y == 1));
                    else if (x == 4) Add(new Queen(new Point(x, y), y == 1));
                    else if (x == 5) Add(new King(new Point(x, y), y == 1));

                    if (y == 1) Add(new Pawn(new Point(x, y + 1), y == 1));
                    else if (y == 8) Add(new Pawn(new Point(x, y - 1), y == 1));
                }
            }
            Refresh();
        }
        public void Refresh()
        {
            Console.SetCursorPosition(0, 0);
            for (int x = 1; x <= 8; x++)
            {
                for (int y = 1; y <= 8; y++)
                {
                    Console.BackgroundColor = new Point(x, y).Color;
                    new Point(x, y).SetCursorPosition();
                    Console.Write("  ");
                }
            }

            foreach (Piece piece in pieces)
            {
                Console.BackgroundColor = piece.Point.Color;
                if (piece.Type != null && piece.Type.Equals("king") && piece.IsUnderAttack()) Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = piece.Color.Item2;
                piece.Point.SetCursorPosition();
                Console.Write(piece);
            }
        }
        public void Add(Piece piece)
        {
            Array.Resize(ref pieces, pieces.Length + 1);
            pieces[pieces.Length - 1] = piece;
            Console.BackgroundColor = piece.Point.Color;
            Console.ForegroundColor = piece.Color.Item2;
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
                if (Find(point) == null) Console.Write("●");
                else Console.Write(Find(point));
            }
        }
        public void Remove(Point point)
        {
            Piece[] temp = new Piece[pieces.Length - 1];
            int index = 0;
            foreach (Piece piece in pieces)
            {
                if (!point.Equals(piece.Point)) temp[index++] = piece;
            }
            pieces = temp;
        }
        public Piece Find(Point point = null, bool? color = null, string type = null)
        {
            foreach (Piece piece in pieces)
            {
                if (piece.Color.Item1 == color || color == null)
                {
                    if (piece.Point.Equals(point) || point == null)
                    {
                        if (piece.GetType().Name == type || type == null) return piece;
                    }
                }
            }
            return null;
        }
        public bool Exists(Point point = null, bool? color = null, string type = null)
        {
            foreach (Piece piece in pieces)
            {
                if (piece.Color.Item1 == color || color == null)
                {
                    if (piece.Point.Equals(point) || point == null)
                    {
                        if (piece.GetType().Name == type || type == null) return true;
                    }
                }
            }
            return false;
        }
    }
}
