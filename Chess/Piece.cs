using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Chess
{
    abstract class Piece
    {
        public Point Point { get; }
        private bool color;
        public ConsoleColor Color
        {
            get
            {
                if (color) return ConsoleColor.White;
                else return ConsoleColor.Black;
            }
        }
        public Point[] Moves
        {
            get
            {
                Point[] moves = new Point[0];

                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        if (IsLegal(new Point(x, y)))
                        {
                            Array.Resize(ref moves, moves.Length + 1);
                            moves[moves.Length - 1] = new Point(x, y);
                        }
                    }
                }

                return moves;
            }
        }
        public Piece(Point point, bool color)
        {
            Point = point;
            this.color = color;
        }
        public abstract bool IsLegal(Point point);
    }
    class Pawn : Piece
    {
        public Pawn(Point point, bool color) : base(point, color) { }
        public override string ToString() => "♟";
        public override bool IsLegal(Point point)
        {
            if (point.Piece != null && point.Piece.Color == Color) return false;

            int direction = 1;
            if (Color == ConsoleColor.Black) direction = -1;

            // small jump
            if (Point.Y + direction == point.Y && Point.X == point.X) return true;
            // big jump
            else if (Point.Y + direction * 2 == point.Y && Point.X == point.X) return true;

            return false;
        }
    }
    class Knight : Piece
    {
        public Knight(Point point, bool color) : base(point, color) { }
        public override string ToString() => "♞ ";
        public override bool IsLegal(Point point)
        {
            if (point.Piece != null && point.Piece.Color == Color) return false;

            if (Math.Abs(point.X - Point.X) == 2 && Math.Abs(point.Y - Point.Y) == 1) return true;
            else if (Math.Abs(point.X - Point.X) == 1 && Math.Abs(point.Y - Point.Y) == 2) return true;

            return false;
        }
    }
    class Bishop : Piece
    {
        public Bishop(Point point, bool color) : base(point, color) { }
        public override string ToString() => "♝ ";
        public override bool IsLegal(Point point)
        {
            if (point.Piece != null && point.Piece.Color == Color) return false;
            return false;
        }
    }
    class Rook : Piece
    {
        public Rook(Point point, bool color) : base(point, color) { }
        public override string ToString() => "♜ ";
        public override bool IsLegal(Point point)
        {
            if (point.Piece != null && point.Piece.Color == Color) return false;
            return false;
        }
    }
    class Queen : Piece
    {
        public Queen(Point point, bool color) : base(point, color) { }
        public override string ToString() => "♛ ";
        public override bool IsLegal(Point point)
        {
            if (point.Piece != null && point.Piece.Color == Color) return false;
            return false;
        }
    }
    class King : Piece
    {
        public King(Point point, bool color) : base(point, color) { }
        public override string ToString() => "♚ ";
        public override bool IsLegal(Point point)
        {
            if (point.Piece != null && point.Piece.Color == Color) return false;
            return false;
        }
    }
}