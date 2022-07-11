using System.Collections.Generic;

namespace Chessington.GameEngine.Pieces
{
    public abstract class Piece
    {
        protected Piece(Player player)
        {
            Player = player;
        }

        public Player Player { get; private set; }

        public abstract IEnumerable<Square> GetAvailableMoves(Board board);

        public virtual void MoveTo(Board board, Square newSquare)
        {
            var currentSquare = board.FindPiece(this);
            board.MovePiece(currentSquare, newSquare);
        }
        
        protected static void RelativeMove(Square currentLoc, int playerMod, List<Square> availableMoves, int colMod, int rowMod)
        {
            Square s = Square.At(currentLoc.Row + (rowMod * playerMod), currentLoc.Col + (colMod * playerMod)); // Gets the appropriate square
            if (s == currentLoc)
            {
                return;
            }
            availableMoves.Add(s); // Adds move to list
        }

        protected static void AbsoluteMove(Square currentLoc, List<Square> availableMoves, int col,
            int row)
        {
            Square s = Square.At(row, col); // Gets the appropriate square
            if (s == currentLoc)
            {
                return;
            }
            availableMoves.Add(s); // Adds move to list
        }
    }
}