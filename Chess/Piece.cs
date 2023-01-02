using System;

namespace Chess
{
    abstract class Piece
    {
        public Point Point { get; set; }
        private bool color;
        public (bool, ConsoleColor) Color
        {
            get
            {
                if (color) return (true, ConsoleColor.White);
                else return (false, ConsoleColor.Black);
            }
        }
        public int PreviousMove { get; set; }
        public int TotalMoves { get; set; }
        public Point[] CurrentMoves
        {
            get
            {
                Point[] moves = new Point[0];

                for (int x = 1; x <= 8; x++)
                {
                    for (int y = 1; y <= 8; y++)
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
            PreviousMove = 0;
        }
        protected bool IsBlocked(Point point)
        {
            int x = Point.X, y = Point.Y;
            while (point.X != x || point.Y != y)
            {
                if (Point.X < point.X) x++;
                else if (Point.X > point.X) x--;
                if (Point.Y < point.Y) y++;
                else if (Point.Y > point.Y) y--;

                if (point.X != x || point.Y != y)
                {
                    if (new Point(x, y).Piece != null) return true;
                }
            }
            return false;
        }
        public abstract bool IsLegal(Point point);
        public bool IsUnderAttack()
        {
            for (int i = 0; i < Program.board.pieces.Length; i++)
            {
                if (Program.board.pieces[i].Color != Color) continue;
                foreach (Point move in Program.board.pieces[i].CurrentMoves)
                {
                    if (Point.Equals(piece.CurrentMoves)) return true;
                }
            }
            return false;
        }
        public virtual void Move(Point point)
        {
            if (point.Piece != null) Program.board.Remove(point);
            Point.X = point.X;
            Point.Y = point.Y;
            PreviousMove = Program.turn;
            TotalMoves++;
            Program.turn++;
        }
    }
    class Pawn : Piece
    {
        public Pawn(Point point, bool color) : base(point, color) { }
        public override string ToString() => "♟";
        public override bool IsLegal(Point point)
        {
            if (point.Piece != null && point.Piece.Color == Color) return false;

            int direction = 1;
            if (!Color.Item1) direction = -1;

            // small jump
            if (point.Piece == null && Point.Y + direction == point.Y && Point.X == point.X) return true;
            // big jump
            if (((Color.Item1 && Point.Y == 2) || (!Color.Item1 && Point.Y == 7)) && Point.Y + direction * 2 == point.Y && Point.X == point.X) return true;
            // Taking opponent pieces
            if (point.Piece != null && Point.Y + direction == point.Y && (Point.X == point.X + 1 || Point.X == point.X - 1)) return true;
            // En passant
            if (new Point(point.X, point.Y - direction).Piece != null)
            {
                Piece enPassant = new Point(point.X, point.Y - direction).Piece;
                if (enPassant.Color != Color
                    && enPassant.PreviousMove == Program.turn - 1
                    && enPassant.TotalMoves == 1
                    && Point.Y + direction == point.Y
                    && (Point.X == point.X + 1 || Point.X == point.X - 1)) return true;
            }
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
            if ((Math.Abs(point.X - Point.X) == Math.Abs(point.Y - Point.Y)) && !IsBlocked(point)) return true;
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
            if ((point.X == Point.X || point.Y == Point.Y) && !IsBlocked(point)) return true;
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
            if ((point.X == Point.X || point.Y == Point.Y) && !IsBlocked(point)) return true;
            if (Math.Abs(point.X - Point.X) == Math.Abs(point.Y - Point.Y) && !IsBlocked(point)) return true;
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
            if (Math.Abs(point.X - Point.X) == 1 && Math.Abs(point.Y - Point.Y) == 1) return true;
            else if (Math.Abs(point.X - Point.X) == 0 && Math.Abs(point.Y - Point.Y) == 1) return true;
            else if (Math.Abs(point.X - Point.X) == 1 && Math.Abs(point.Y - Point.Y) == 0) return true;
            return false;
        }
    }
}