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
        
        protected static void StandardMove(Square currentLoc, int playerMod, List<Square> availableMoves, int colMod, int rowMod)
        {
            Square s = Square.At(currentLoc.Row + rowMod * playerMod, currentLoc.Col + colMod * playerMod); // Gets the appropriate square
            availableMoves.Add(s); // Adds move to list
        }
    }
}